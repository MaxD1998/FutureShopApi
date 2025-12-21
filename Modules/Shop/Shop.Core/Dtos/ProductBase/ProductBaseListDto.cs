using Shop.Domain.Entities.ProductBases;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseListDto
{
    public string CategoryName { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public int ProductCount { get; set; }

    public int ProductParameterCount { get; set; }

    public static Expression<Func<ProductBaseEntity, ProductBaseListDto>> Map() => entity => new()
    {
        CategoryName = entity.Category.Name,
        Id = entity.Id,
        Name = entity.Name,
        ProductCount = entity.Products.Count(),
        ProductParameterCount = entity.ProductParameters.Count(),
    };
}