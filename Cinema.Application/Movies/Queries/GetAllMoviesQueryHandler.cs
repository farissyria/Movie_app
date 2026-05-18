using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Application.Services;
using Cinema.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Movies.Queries
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<MovieDto>>
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public GetAllMoviesQueryHandler(IMovieService movieService, IMapper mapper)
        {
             _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<List<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return _mapper.Map<List<MovieDto>>(movies);
        }
    }
}
