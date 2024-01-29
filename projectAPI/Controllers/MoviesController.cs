using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectAPI.BL;
using projectAPI.Model;

namespace projectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovie movie;
        private readonly IGenre genre;
        private readonly IMapper mapper;
        private readonly List<string> allowextintion = new List<string> { ".jpg", ".png" };
        private readonly long maxAllowPosterSize = 1048576;
        public MoviesController(IMovie movie,IGenre genre,IMapper mapper)
        {
            this.movie = movie;
            this.genre = genre;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await movie.GetAllMovies();

            var data = mapper.Map<IEnumerable<MovieDetailesDTO>>(movies);

            return Ok(data);
        }

        [HttpGet("{id}",Name ="moviedetailesroute")]
        public async Task<IActionResult> GetById(int id)
        {  
           var moviefound=await movie.GetMovieById(id);
            if (moviefound != null)
            {
                var data=mapper.Map<MovieDetailesDTO>(moviefound);
                return Ok(data);    
            }
            return BadRequest("Invalid ID!");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm]MovieCreateDTO movieadedd)
        {
          
            if (!allowextintion.Contains(Path.GetExtension(movieadedd.Poster.FileName).ToLower()))
                return BadRequest("only .png and .jpg are allowed");

            if (movieadedd.Poster.Length > maxAllowPosterSize)
                return BadRequest("max allow size for poster is 1MB");

            var validGenre = await genre.IsValidGenre(movieadedd.GenreId);
            if (!validGenre)
                return BadRequest("Invalid Genre Id!");
            

            using var datastream = new MemoryStream();
            movieadedd.Poster.CopyTo(datastream);

            var data = mapper.Map<Movie>(movieadedd);
            data.Poster=datastream.ToArray();

            return Ok(await movie.AddMovie(data));
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatemovie(int id , [FromForm]MovieUpdateDTO movieupdate)
        {
            var moviefound = await movie.GetMovieById(id);

            if (moviefound == null)
                return BadRequest("invalid movie id!");
         

            var validgenre = await genre.IsValidGenre(movieupdate.GenreId);
            if (!validgenre)
                return BadRequest("invalid genre id!");
            

            if (movieupdate.Poster != null)
            {
                if (!allowextintion.Contains(Path.GetExtension(movieupdate.Poster.FileName.ToLower())))
                    return BadRequest("only .png and .jpg are allowed");
                

                if (movieupdate.Poster.Length > maxAllowPosterSize)
                   return BadRequest("max allow size for poster is 1mb");

                using var datastream = new MemoryStream();
                movieupdate.Poster.CopyTo(datastream);
                moviefound.Poster = datastream.ToArray();
            }

            moviefound.Title = movieupdate.Title;
            moviefound.Year = movieupdate.Year;
            moviefound.StoryLine = movieupdate.StoryLine;
            moviefound.Rate = movieupdate.Rate;
            moviefound.GenreId = movieupdate.GenreId;

            return Ok(movie.UpdateMovie(moviefound));

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             var moviefound=await movie.GetMovieById(id);
            if (moviefound == null)
                return BadRequest("Inavlid id!");

            return Ok(movie.delete(moviefound));  
             
             
        }
    }
}
