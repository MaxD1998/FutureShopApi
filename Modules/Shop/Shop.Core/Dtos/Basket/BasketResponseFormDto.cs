using Shop.Core.Dtos.Basket.BasketItem;
using Shop.Domain.Entities.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketResponseFormDto : BasketRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<BasketEntity, BasketResponseFormDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemFormDto.Map()).ToList(),
        Id = entity.Id,
    };
}