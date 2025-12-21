using Shop.Core.Dtos.ProductBase.ProductParameter;
using Shop.Infrastructure.Persistence.Entities.ProductBases;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseResponseFormDto : ProductBaseRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<ProductBaseEntity, ProductBaseResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        CategoryId = entity.CategoryId,
        Name = entity.Name,
        ProductParameters = entity.ProductParameters.AsQueryable().Select(ProductParameterFormDto.Map()).ToList(),
    };
}