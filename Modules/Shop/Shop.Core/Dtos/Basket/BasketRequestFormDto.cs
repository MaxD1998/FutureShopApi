using Shop.Core.Dtos.Basket.BasketItem;
using Shop.Infrastructure.Persistence.Entities.Baskets;

namespace Shop.Core.Dtos.Basket;

public class BasketRequestFormDto
{
    public List<BasketItemFormDto> BasketItems { get; set; }

    public BasketEntity ToEntity(Guid? userId) => new()
    {
        BasketItems = BasketItems.Select(x => x.ToEntity()).ToList(),
        UserId = userId
    };
}