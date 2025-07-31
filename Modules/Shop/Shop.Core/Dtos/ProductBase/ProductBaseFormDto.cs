using Shop.Core.Dtos.ProductParameter;
using Shop.Domain.Aggregates.ProductBases;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public List<ProductParameterFormDto> ProductParameters { get; set; }

    public static Expression<Func<ProductBaseAggregate, ProductBaseFormDto>> Map() => entity => new()
    {
        CategoryId = entity.CategoryId,
        Name = entity.Name,
        ProductParameters = entity.ProductParameters.AsQueryable().Select(ProductParameterFormDto.Map()).ToList(),
    };

    public ProductBaseAggregate ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
        ProductParameters = ProductParameters.Select(x => x.ToEntity()).ToList(),
    };
}