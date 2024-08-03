using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductPhoto;

public class ProductPhotoFormDto
{
    public ProductPhotoFormDto(ProductPhotoEntity entity)
    {
        FileId = entity.FileId;
        Position = entity.Position;
    }

    public string FileId { get; set; }

    public int Position { get; set; }

    public ProductPhotoEntity ToEntity() => new()
    {
        FileId = FileId,
        Position = Position,
    };
}