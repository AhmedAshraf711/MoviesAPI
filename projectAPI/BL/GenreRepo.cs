


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











        //public List<Genre> GetAllGenres()
        //{
        //    return applicationdbcontext.genres.OrderBy(o => o.Name).ToList();
        //}

        //public Genre GetGenreById(int id)
        //{
        //    return applicationdbcontext.genres.FirstOrDefault(g => g.Id == id);
        //}

        //public void AddGenre(GenreDTO genredto)
        //{
        //    var genre = new Genre() { Name = genredto.Name };

        //    applicationdbcontext.genres.Add(genre);
        //    applicationdbcontext.SaveChanges();
        //}

        //public void UpdateGenre(int id,GenreDTO genredto)
        //{
        //    var genrefound = GetGenreById(id);
        //    if (genrefound != null)
        //    {
        //        genrefound.Name=genredto.Name;
        //        applicationdbcontext.genres.Update(genrefound);
        //        applicationdbcontext.SaveChanges();
        //    }
        //}

        //public void DeleteGenre(int id)
        //{
        //    var genrefound=GetGenreById(id);    
        //    if(genrefound != null)
        //    {
        //        applicationdbcontext.Remove(genrefound);
        //        applicationdbcontext.SaveChanges();
        //    }
        //}
    }
}
