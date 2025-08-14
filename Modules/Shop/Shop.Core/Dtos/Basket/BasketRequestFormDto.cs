using Shop.Core.Dtos.BasketItem;
using Shop.Infrastructure.Entities;

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