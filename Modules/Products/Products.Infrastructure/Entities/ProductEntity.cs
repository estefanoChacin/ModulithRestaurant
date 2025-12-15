using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.Infrastructure.Entities;

public class ProductEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonRequired]
    [BsonElement("name")]
    public required string Name { get; set; }
    [BsonRequired]
    [BsonElement("stock")]
    public required int Stock { get; set; }
    [BsonRequired]
    [BsonElement("price")]
    public required decimal Price { get; set; }
    [BsonElement("createdate")]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}
