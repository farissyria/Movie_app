using Cinema.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto?> GetMovieByIdAsync(string id);
        Task<MovieDto> CreateMovieAsync(CreateMovieDto createDto);
        Task UpdateMovieAsync(string id, UpdateMovieDto updateDto);
        Task DeleteMovieAsync(string id);
        Task<IEnumerable<MovieDto>> SearchMoviesAsync(string searchTerm);
        Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre);
       
        Task<IEnumerable<MovieDto>> GetTopRatedAsync(int count);
    }
}
