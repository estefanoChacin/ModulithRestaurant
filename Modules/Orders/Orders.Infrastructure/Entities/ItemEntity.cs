using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Orders.Infrastructure.Entities;

public class ItemEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonElement("quantity")]
    [BsonRequired]
    public required int Quantity { get; set; }
}
