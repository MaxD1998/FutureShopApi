using Product.Core.Dtos.ProductPhoto;
using Product.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Product;

public class ProductResponseFormDto : ProductRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<ProductEntity, ProductResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        ProductBaseId = entity.ProductBaseId,
        ProductPhotos = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(ProductPhotoFormDto.Map()).ToList(),
    };
}