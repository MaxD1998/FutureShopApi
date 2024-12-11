using FluentValidation;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductPhoto;

public class ProductPhotoFormDto
{
    public string FileId { get; set; }

    public Guid? Id { get; set; }

    public static Expression<Func<ProductPhotoEntity, ProductPhotoFormDto>> Map() => entity => new()
    {
        FileId = entity.FileId,
        Id = entity.Id,
    };

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