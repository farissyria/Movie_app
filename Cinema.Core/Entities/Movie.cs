using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Cinema.Core.Entities
{
    [CollectionName("Movies")]
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }  

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("genres")]
        public List<string> Genres { get; set; } = new List<string>();

        [BsonElement("durationMinutes")]
        public int DurationMinutes { get; set; }

       [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("rating")]
        public double Rating { get; set; }  // IMDb-like rating 0-10

        [BsonElement("language")]
        public string Language { get; set; } = "English";
                
    }
}
