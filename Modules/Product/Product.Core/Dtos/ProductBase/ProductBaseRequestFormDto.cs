using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseRequestFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public ProductBaseEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
    };
}