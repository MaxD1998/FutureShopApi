using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Bases;

namespace Product.Domain.Documents;

public class ProductPhotoDocument : BaseFileDocument
{
    [BsonElement(Order = 100)]
    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public Guid ProductId { get; set; }
}