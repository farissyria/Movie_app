using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Core.Entities;
using Cinema.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetMovieByIdAsync(string id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie == null ? null : _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createDto)
        {
            var movie = _mapper.Map<Movie>(createDto);
           

            var created = await _movieRepository.AddAsync(movie);
            return _mapper.Map<MovieDto>(created);
        }

        public async Task UpdateMovieAsync(string id, UpdateMovieDto updateDto)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with id {id} not found");

            _mapper.Map(updateDto, movie);
            
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task DeleteMovieAsync(string id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with id {id} not found");

           
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string searchTerm)
        {
            var movies = await _movieRepository.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(string genre)
        {
            var movies = await _movieRepository.GetByGenreAsync(genre);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> GetTopRatedAsync(int count)
        {
            var movies = await _movieRepository.GetTopRatedAsync(count);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
    }
}
