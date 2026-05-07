using Cinema.Application.DTOs;

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
