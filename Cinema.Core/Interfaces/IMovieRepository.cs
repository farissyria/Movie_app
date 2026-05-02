using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetTopRatedAsync(int count);
    }
}
