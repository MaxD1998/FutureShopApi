using Shop.Infrastructure.Persistence.Entities.Promotions;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Promotion.PromotionProduct;

public class PromotionProductFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public static Expression<Func<PromotionProductEntity, PromotionProductFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        ProductName = entity.Product.Name
    };

    public PromotionProductEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId
    };
}