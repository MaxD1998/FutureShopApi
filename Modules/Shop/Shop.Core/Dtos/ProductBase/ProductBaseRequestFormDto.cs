using Shop.Core.Dtos.ProductBase.ProductParameter;
using Shop.Domain.Entities.ProductBases;

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