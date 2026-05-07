using Cinema.Core.Entities;

namespace Cinema.Core.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetTopRatedAsync(int count);
    }
}
