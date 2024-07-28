using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Bases;

namespace Product.Domain.Documents;

public class ProductPhotoDocument : BaseDocument
{
    [BsonElement(Order = 105)]
    [BsonRepresentation(BsonType.String)]
    public byte[] Data { get; set; }

    [BsonElement(Order = 101)]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [BsonElement(Order = 100)]
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
}