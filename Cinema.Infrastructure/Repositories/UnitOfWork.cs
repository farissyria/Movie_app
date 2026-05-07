using Cinema.Core.Entities;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;
        private IRepository<Movie> _Movies;
        private IMovieRepository _MovieRepo;
        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
        }
        public IRepository<Movie> Movies => _Movies ??=new Repository<Movie>(_context.Movies);

        public IMovieRepository MovieRepo => _MovieRepo ??=new MovieRepository(_context);

        

        public async Task<int> CompleteAsync()
        {
            return await Task.FromResult(0);
        }

        public void Dispose()
        {
          
        }
    }
}
