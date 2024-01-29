using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectAPI.BL;

namespace projectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenre genre;

        public GenresController(IGenre genre)
        {
            this.genre = genre;
        }

        [HttpGet]
        public async Task<IActionResult> GetALLGenres()
        {
            return Ok(await genre.GetAllGenres());
        }

        [HttpGet("{id}",Name ="GenreDetailesRoute")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var foundgenre= await genre.GetGenreById(id);
            if (foundgenre == null)
                return BadRequest("Invalid Id!");
            return Ok(foundgenre);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDTO genredto)
        {
            if(ModelState.IsValid)
            {
               Genre newgenre = new Genre() { Name = genredto.Name };
               await genre.AddGenre(newgenre);
               var url = Url.Link("GenreDetailesRoute", newgenre);
               return Created(url, newgenre);
            }
             return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id , GenreDTO genredto)
        {
            var oldgenre = await genre.GetGenreById(id);
            if (oldgenre != null)
            {
                 oldgenre.Name = genredto.Name;
                  genre.UpdateGenre(oldgenre);
                 return Ok(oldgenre);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genreFound=await genre.GetGenreById(id);
            if(genreFound == null)
                return BadRequest("Invalid Id!");
            genre.DeleteGenre(genreFound);
            return Ok(genreFound); 
        }
         
    }
}
