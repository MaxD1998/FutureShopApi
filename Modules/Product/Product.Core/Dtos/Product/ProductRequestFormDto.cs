using Product.Core.Dtos.ProductPhoto;
using Product.Infrastructure.Entities;

namespace Product.Core.Dtos.Product;

public class ProductRequestFormDto
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductPhotoFormDto> ProductPhotos { get; set; }

    public ProductEntity ToEntity() => new()
    {
        Name = Name,
        ProductBaseId = ProductBaseId,
        ProductPhotos = ProductPhotos.Select((x, index) => x.ToEntity(index)).ToList(),
    };
}