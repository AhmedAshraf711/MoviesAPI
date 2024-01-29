
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