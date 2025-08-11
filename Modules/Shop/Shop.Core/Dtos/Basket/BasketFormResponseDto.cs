using Shop.Core.Dtos.BasketItem;
using Shop.Domain.Aggregates.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketFormResponseDto : BasketFormRequestDto
{
    public Guid Id { get; set; }

    public static Expression<Func<BasketAggregate, BasketFormResponseDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemFormDto.Map()).ToList(),
    };
}