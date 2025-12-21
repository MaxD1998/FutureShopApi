using Shop.Infrastructure.Persistence.Entities.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket.BasketItem;

public class BasketItemFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public static Expression<Func<BasketItemEntity, BasketItemFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        Quantity = entity.Quantity,
    };

    public BasketItemEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId,
        Quantity = Quantity,
    };
}