using Shop.Domain.Aggregates.Products.Entities;

namespace Shop.Core.Dtos.ProductPhoto;

public class ProductPhotoEventDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    public ProductPhotoEntity Map() => new()
    {
        ExternalId = Id,
        FileId = FileId,
        Position = Position,
    };
}