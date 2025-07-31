using Product.Domain.Aggregates.ProductBases;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public static Expression<Func<ProductBaseAggregate, ProductBaseFormDto>> Map() => entity => new()
    {
        CategoryId = entity.CategoryId,
        Name = entity.Name,
    };

    public ProductBaseAggregate ToEntity() => new(CategoryId, Name);
}