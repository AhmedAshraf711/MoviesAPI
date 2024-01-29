using Microsoft.AspNetCore.Mvc;

namespace projectAPI.BL
{
    public interface IMovie 
    {
         Task<IEnumerable<Movie>> GetAllMovies();
         Task<Movie> GetMovieById(int id);
         Task<Movie> AddMovie(Movie movie);
         Movie UpdateMovie(Movie movie);
         Movie delete(Movie movie);
    }
}
