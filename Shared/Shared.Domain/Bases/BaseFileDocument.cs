using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Domain.Bases;

public class BaseFileDocument : BaseDocument
{
    [BsonElement(Order = 52)]
    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public string ContentType { get; set; }

    [BsonElement(Order = 51)]
    [BsonRepresentation(BsonType.Binary)]
    [BsonRequired]
    public byte[] Data { get; set; }

    [BsonElement(Order = 54)]
    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public string EntityType { get; set; }

    [BsonElement(Order = 53)]
    [BsonRepresentation(BsonType.Int64)]
    [BsonRequired]
    public long Length { get; set; }

    [BsonElement(Order = 50)]
    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public string Name { get; set; }
}