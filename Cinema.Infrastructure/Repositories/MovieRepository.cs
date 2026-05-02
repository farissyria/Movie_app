using Cinema.Core.Entities;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MongoDbContext context) : base(context.Movies)
        {
        }

        public async Task<IEnumerable<Movie>> SearchAsync(string searchTerm)
        {
            var filter = Builders<Movie>.Filter.Or(
                Builders<Movie>.Filter.Regex(m => m.Title, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
              
            );

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByGenreAsync(string genre)
        {
            var filter = Builders<Movie>.Filter.AnyEq(m => m.Genres, genre);
            return await _collection.Find(filter).ToListAsync();
        }
        
        public async Task<IEnumerable<Movie>> GetTopRatedAsync(int count)
        {
            return await _collection.Find(_ => true)
                .SortByDescending(m => m.Rating)
                .Limit(count)
                .ToListAsync();
        }
    }
}
