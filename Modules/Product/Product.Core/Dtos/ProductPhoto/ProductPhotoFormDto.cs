using FluentValidation;
using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductPhoto;

public class ProductPhotoFormDto
{
    public ProductPhotoFormDto()
    {
    }

    public ProductPhotoFormDto(ProductPhotoEntity entity)
    {
        FileId = entity.FileId;
        Id = entity.Id;
    }

    public string FileId { get; set; }

    public Guid? Id { get; set; }

    public ProductPhotoEntity ToEntity(int index) => new()
    {
        Id = Id ?? Guid.Empty,
        FileId = FileId,
        Position = index,
    };
}

public class ProductPhotoFormValidator : AbstractValidator<ProductPhotoFormDto>
{
}