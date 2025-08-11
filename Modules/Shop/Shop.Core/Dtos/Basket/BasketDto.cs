using Shop.Core.Dtos.BasketItem;
using Shop.Domain.Aggregates.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; }

    public Guid Id { get; set; }

    public static Expression<Func<BasketAggregate, BasketDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemDto.Map()).ToList(),
        Id = entity.Id,
    };
}