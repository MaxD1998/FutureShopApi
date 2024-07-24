using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace File.Domain.Entities;

public class ProductPhotoEntity
{
    public byte[] Data { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
}