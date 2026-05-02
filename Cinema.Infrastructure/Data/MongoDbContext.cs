using AspNetCore.Identity.MongoDbCore.Models;
using Cinema.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var databaseName = configuration["MongoDB:DatabaseName"] ?? "CinemaDB";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            CreateCollectionIfNotExists("Movies");
            CreateCollectionIfNotExists("Users");
            CreateCollectionIfNotExists("Roles");
        }

        private void CreateCollectionIfNotExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = _database.ListCollectionNames(new ListCollectionNamesOptions { Filter = filter });

            if (!collections.Any())
            {
                _database.CreateCollection(collectionName);
                Console.WriteLine($"Created collection: {collectionName}");
            }
        }

        public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("Movies");

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public IMongoCollection<Roles> Roles => _database.GetCollection<Roles>("Roles");

    }
}