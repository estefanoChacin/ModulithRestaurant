using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Customers.Infrastructure.Entities;

public class CustomerEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonRequired]
    [BsonElement("name")]
    public required string Name { get; set; }
    [BsonRequired]
    [BsonElement("email")]
    public required string Email { get; set; }
}
