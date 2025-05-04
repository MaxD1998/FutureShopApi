using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseEventDto
{
    public Guid CategoryId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public ProductBaseEntity Map(ShopContext context) => new()
    {
        CategoryId = context.Set<CategoryEntity>().Where(x => x.ExternalId == CategoryId).Select(x => x.Id).FirstOrDefault(),
        ExternalId = Id,
        Name = Name,
    };
}