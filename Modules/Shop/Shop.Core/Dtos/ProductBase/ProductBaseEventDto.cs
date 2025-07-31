using Shop.Domain.Aggregates.ProductBases;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseEventDto
{
    public Guid CategoryId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public ProductBaseAggregate Map() => new()
    {
        ExternalId = Id,
        Name = Name,
    };
}