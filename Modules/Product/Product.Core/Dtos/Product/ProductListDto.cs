using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Product;

public class ProductListDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public static Expression<Func<ProductEntity, ProductListDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
    };
}