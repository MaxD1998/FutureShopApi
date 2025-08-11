using Shop.Core.Dtos.Product;
using Shop.Domain.Aggregates.Baskets.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.BasketItem;

public class BasketItemDto
{
    public Guid Id { get; set; }

    public ProductBasketItemDto Product { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public static Expression<Func<BasketItemEntity, BasketItemDto>> Map()
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
        };
    }
}