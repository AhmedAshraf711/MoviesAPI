

namespace projectAPI.BL
{
    public interface IGenre
    {
       Task<IEnumerable<Genre>> GetAllGenres();
       Task<Genre> GetGenreById(int id);
       Task<Genre> AddGenre(Genre genre);
       Genre UpdateGenre(Genre genre);
       Genre DeleteGenre(Genre genre);
        Task<bool> IsValidGenre(int id);
    }
}
