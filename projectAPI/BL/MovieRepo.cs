
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectAPI.Model;
using System;

namespace projectAPI.BL
{
    public class MovieRepo : IMovie 
    {
        private readonly Applicationdbcontext applicationdbcontext;

        public MovieRepo(Applicationdbcontext applicationdbcontext)
        {
            this.applicationdbcontext = applicationdbcontext;
        }

       public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies =await applicationdbcontext.movies
                        .OrderByDescending(x => x.Rate)
                        .Include(m => m.Genre)
                        .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await applicationdbcontext.movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
           await applicationdbcontext.movies.AddAsync(movie);
           applicationdbcontext.SaveChanges();
           return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            applicationdbcontext.movies.Update(movie);
            applicationdbcontext.SaveChanges();
            return (movie);
        }

        public Movie delete(Movie movie)
        {
            applicationdbcontext.movies.Remove(movie);
            applicationdbcontext.SaveChanges();
            return (movie);
        }
    }
}

        //public class BadRequestException : Exception
        //{
        //    public BadRequestException(string message) : base(message)
        //    {
        //    }
        //}

        ////public List<MovieDetailesDTO> GetAllMovies()
        ////{
        ////    var movies= applicationdbcontext.movies
        ////                .OrderByDescending(x => x.Rate)
        ////                .Include(m=>m.Genre)
        ////                .Select(m=>new MovieDetailesDTO
        ////                {
        ////                    //GenreId=m.Genre.Id,
        ////                    Id = m.Id,
        ////                    GenreId = m.GenreId,
        ////                    GenreName =m.Genre.Name,
        ////                    Title =m.Title,
        ////                    Year =m.Year,
        ////                    Rate =m.Rate,
        ////                    StoryLine =m.StoryLine,
        ////                    Poster = m.Poster
        ////                }).ToList();
        ////           return movies;
        ////}
//public void delete(int id)
//{
//    var moviefound = applicationdbcontext.movies.FirstOrDefault(m=>m.Id==id);
//    if (moviefound == null) 
//    {
//        throw new BadRequestException("Invalid Movie Id!");
//    }
//    applicationdbcontext.movies.Remove(moviefound);
//    applicationdbcontext.SaveChanges();

//}


////public MovieDetailesDTO GetMovieById(int? id)
////{
////    var movie = applicationdbcontext.movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
////    if (movie == null)
////    {
////        throw new BadRequestException("Invalid Movie Id!");
////    }
////    var dto = new MovieDetailesDTO
////    {
////        Id = movie.Id,
////        GenreId = movie.GenreId,
////        GenreName = movie.Genre.Name,
////        Title = movie.Title,
////        Year = movie.Year,
////        Rate = movie.Rate,
////        StoryLine = movie.StoryLine,
////        Poster = movie.Poster
////    };
////public void AddMovie( [FromForm] MovieCreateDTO movie)
////{
////   List<string> allowextintion = new List<string> {".jpg",".png" };
////    long maxAllowPosterSize = 1048576;
////    if (!allowextintion.Contains(Path.GetExtension(movie.Poster.FileName).ToLower()))
////    {
////        // If the condition is not met, return a response with a 400 Bad Request status code
////        throw new BadRequestException("only .png and .jpg are allowed");
////    }

////    if (movie.Poster.Length>maxAllowPosterSize) 
////    {

////        throw new BadRequestException("max allow size for poster is 1MB");
////    }

////    var validGenre = applicationdbcontext.genres.Any(g => g.Id == movie.GenreId);
////    if (!validGenre)
////    {
////        throw new BadRequestException("Invalid Genre Id!");
////    }

////    using var datastream = new MemoryStream();
////    movie.Poster.CopyTo(datastream);

////    var newMovie = new Movie()
////    {
////        GenreId = movie.GenreId,
////        Rate = movie.Rate,
////        StoryLine = movie.StoryLine,
////        Title=movie.Title,
////        Year=movie.Year,
////        Poster=datastream.ToArray()
////    };

////    applicationdbcontext.movies.Add(newMovie);
////    applicationdbcontext.SaveChanges();
////}
////    return dto;
///
////}





        ////public MovieUpdateDTO UpdateMovie(int id ,MovieUpdateDTO movie)
        ////{
        ////    var moviefound = applicationdbcontext.movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
        ////    if (moviefound == null)
        ////    {
        ////        throw new BadRequestException("Invalid Movie Id!");
        ////    }

        ////    List<string> allowextintion = new List<string> { ".jpg", ".png" };
        ////    long maxAllowPosterSize = 1048576;

        ////    var validGenre = applicationdbcontext.genres.Any(g => g.Id == moviefound.GenreId);
        ////    if (!validGenre)
        ////    {
        ////        throw new BadRequestException("Invalid Genre Id!");
        ////    }

        ////    if(movie.Poster != null)
        ////    {
        ////        if (!allowextintion.Contains(Path.GetExtension(movie.Poster.FileName.ToLower())))
        ////       {
        ////          throw new BadRequestException("only .png and .jpg are allowed");
        ////       }

        ////      if (movie.Poster.Length > maxAllowPosterSize)
        ////      {

        ////        throw new BadRequestException("max allow size for poster is 1MB");
        ////      }

        ////        using var datastream = new MemoryStream();
        ////        movie.Poster.CopyTo(datastream);
        ////        moviefound.Poster=datastream.ToArray();
        ////    }
             
        ////    moviefound.Title = movie.Title;
        ////    moviefound.Year = movie.Year;
        ////    moviefound.StoryLine = movie.StoryLine;
        ////    moviefound.Rate = movie.Rate;
        ////    moviefound.GenreId = movie.GenreId;

        ////    applicationdbcontext.movies.Update(moviefound);
        ////    applicationdbcontext.SaveChanges();
        ////    return (movie);

        ////}