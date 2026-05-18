using Cinema.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Movies.Commands
{
    public class CreateMovieCommand: IRequest<MovieDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Genres { get; set; } = new List<string>();
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string Language { get; set; } = "English";
    }
}
