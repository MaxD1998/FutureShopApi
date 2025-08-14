using Shop.Core.Dtos.ProductParameter;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseRequestFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public List<ProductParameterFormDto> ProductParameters { get; set; }

    public ProductBaseEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
        ProductParameters = ProductParameters.Select(x => x.ToEntity()).ToList(),
    };
}