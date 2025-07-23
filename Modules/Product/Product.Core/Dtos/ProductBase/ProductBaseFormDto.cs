using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public static Expression<Func<Domain.Aggregates.ProductBases.ProductBaseAggregate, ProductBaseFormDto>> Map() => entity => new()
    {
        CategoryId = entity.CategoryId,
        Name = entity.Name,
    };

    public Domain.Aggregates.ProductBases.ProductBaseAggregate ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
    };
}