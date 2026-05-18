using Cinema.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Movies.Queries
{
    public class GetAllMoviesQuery:IRequest<List<MovieDto>>
    {
    }
}
