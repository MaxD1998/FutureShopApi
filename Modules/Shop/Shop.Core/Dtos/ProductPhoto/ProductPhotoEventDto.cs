using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Dtos.ProductPhoto;

public class ProductPhotoEventDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public int Position { get; set; }

    public Guid ProductId { get; set; }

    public ProductPhotoEntity Map(ShopContext context) => new()
    {
        ExternalId = Id,
        FileId = FileId,
        Position = Position,
        ProductId = context.Set<ProductEntity>().Where(x => x.ExternalId == ProductId).Select(x => x.Id).FirstOrDefault(),
    };
}