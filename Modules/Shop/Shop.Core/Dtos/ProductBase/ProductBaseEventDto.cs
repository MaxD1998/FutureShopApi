using Shop.Infrastructure.Persistence.Entities.ProductBases;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseEventDto
{
    public Guid CategoryId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public ProductBaseEntity Map() => new()
    {
        ExternalId = Id,
        Name = Name,
    };
}