using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Core.Entities;
using Cinema.Core.Interfaces;
using MediatR;

namespace Cinema.Application.Movies.Commands
{
    // ✅ Change the return type from CreateMovieDto to MovieDto
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ✅ Return MovieDto (not CreateMovieDto)
        public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                Genres = request.Genres,
                DurationMinutes = request.DurationMinutes,
                ReleaseDate = request.ReleaseDate,
                Rating = request.Rating,
                Language = request.Language,
             
            };

            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.CompleteAsync();

            // ✅ Map to MovieDto (includes Id)
            return _mapper.Map<MovieDto>(movie);
        }
    }
}