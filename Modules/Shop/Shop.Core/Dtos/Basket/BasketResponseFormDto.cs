using Shop.Core.Dtos.BasketItem;
using Shop.Infrastructure.Entities;
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