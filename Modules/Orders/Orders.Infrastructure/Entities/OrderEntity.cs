using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Orders.Infrastructure.Entities;

public class OrderEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonElement("products")]
    [BsonRequired]
    public required List<ItemEntity> Products { get; set; }
    [BsonElement("total")]
    [BsonRequired]
    public decimal Total { get; set; }
    [BsonElement("idCustomer")]
    [BsonRequired]
    public required string IdCustomer{ get; set; }
}
