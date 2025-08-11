using Shop.Core.Dtos.BasketItem;
using Shop.Domain.Aggregates.Baskets;

namespace Shop.Core.Dtos.Basket;

public class BasketFormRequestDto
{
    public List<BasketItemFormDto> BasketItems { get; set; }

    public BasketAggregate ToEntity(Guid? userId) => new(userId, BasketItems.Select(x => x.ToEntity()).ToList());
}