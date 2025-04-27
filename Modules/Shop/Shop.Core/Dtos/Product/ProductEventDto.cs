using Shop.Core.Dtos.ProductPhoto;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Dtos.Product;

public class ProductEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductPhotoEventDto> ProductPhotos { get; set; }

    public ProductEntity Map(ShopPostgreSqlContext context) => new()
    {
        ExternalId = Id,
        Name = Name,
        ProductBaseId = context.Set<ProductBaseEntity>().Where(x => x.ExternalId == ProductBaseId).Select(x => x.Id).FirstOrDefault(),
        ProductPhotos = ProductPhotos.Select(x => x.Map(context)).ToList(),
    };
}