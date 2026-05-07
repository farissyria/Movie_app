using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Cinema.Core.Entities
{
    [CollectionName("Roles")]
    public class Roles : MongoIdentityRole<Guid>
    {
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
