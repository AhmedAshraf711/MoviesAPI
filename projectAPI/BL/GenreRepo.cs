


using Microsoft.EntityFrameworkCore;
using projectAPI.DTO;

namespace projectAPI.BL
{
    public class GenreRepo:IGenre
    {

        private readonly Applicationdbcontext applicationdbcontext;

        public GenreRepo(Applicationdbcontext applicationdbcontext)
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await applicationdbcontext.genres.OrderBy(g=>g.Id).ToListAsync();

        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await applicationdbcontext.genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> AddGenre(Genre genre)
        {

            await applicationdbcontext.genres.AddAsync(genre);
            applicationdbcontext.SaveChanges();

            return genre;
        }

        public Genre UpdateGenre(Genre genre)
        {

            applicationdbcontext.Update(genre);
            applicationdbcontext.SaveChanges();

            return genre;
        }

        public Genre DeleteGenre(Genre genre)
        {
             applicationdbcontext.genres.Remove(genre);
             applicationdbcontext.SaveChanges();
             return genre;
        }

        public async Task<bool> IsValidGenre(int id)
        {
            return await applicationdbcontext.genres.AnyAsync(g => g.Id == id);    
        }

    }
}
