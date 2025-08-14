using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Infrastructure.Bases;

public class BaseDocument
{
    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement(Order = 2)]
    [BsonRequired]
    public DateTime CreateTime { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement(Order = 1)]
    public string Id { get; set; }
}