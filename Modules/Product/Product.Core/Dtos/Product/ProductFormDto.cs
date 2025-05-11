using Product.Core.Dtos.ProductPhoto;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Product;

public class ProductFormDto
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductPhotoFormDto> ProductPhotos { get; set; }

    public static Expression<Func<ProductEntity, ProductFormDto>> Map() => entity => new()
    {
        Name = entity.Name,
        ProductBaseId = entity.ProductBaseId,
        ProductPhotos = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(ProductPhotoFormDto.Map()).ToList(),
    };

    public ProductEntity ToEntity() => new()
    {
        Name = Name,
        ProductBaseId = ProductBaseId,
        ProductPhotos = ProductPhotos.Select((x, index) => x.ToEntity(index)).ToList(),
    };
}