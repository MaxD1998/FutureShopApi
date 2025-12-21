using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseResponseFormDto : ProductBaseRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<ProductBaseEntity, ProductBaseResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        CategoryId = entity.CategoryId,
        Name = entity.Name,
    };
}