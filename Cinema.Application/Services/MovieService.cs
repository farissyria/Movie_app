using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Core.Entities;
using Cinema.Core.Interfaces;

namespace Cinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        public MovieService(IUnitOfWork unitOfWorky, IMapper mapper, ICacheService cache)
        {
            _unitOfWork = unitOfWorky;
            _mapper = mapper;
          _cache = cache;
        }
      

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            string cacheKey = "Get_all_movies";
            var cachedMovies = await _cache.GetAll<IEnumerable<MovieDto>>(cacheKey);
            if (cachedMovies != null)
                return cachedMovies;


            var movies = await _unitOfWork.Movies.GetAllAsync();
            //return _mapper.Map<IEnumerable<MovieDto>>(movies);
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            await _cache.SetAsync(cacheKey, movieDtos, 10);
            return movieDtos;
        }

        public async Task<MovieDto?> GetMovieByIdAsync(string id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            return movie == null ? null : _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createDto)
        {
            var movie = _mapper.Map<Movie>(createDto);
           

            var created = await _unitOfWork.Movies.AddAsync(movie);
            return _mapper.Map<MovieDto>(created);
        }

        public async Task UpdateMovieAsync(string id, UpdateMovieDto updateDto)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with id {id} not found");

            _mapper.Map(updateDto, movie);
            
            await _unitOfWork.Movies.UpdateAsync(movie);
        }

        public async Task DeleteMovieAsync(string id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with id {id} not found");

           
            await _unitOfWork.Movies.UpdateAsync(movie);
        }

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string searchTerm)
        {
            var movies = await _unitOfWork.MovieRepo.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre)
        {
            var movies = await _unitOfWork.MovieRepo.GetByGenreAsync(genre);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> GetTopRatedAsync(int count)
        {
            var movies = await _unitOfWork.MovieRepo.GetTopRatedAsync(count);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
    }
}
