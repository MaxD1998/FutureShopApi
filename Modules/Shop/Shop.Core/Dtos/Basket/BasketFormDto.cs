using Shop.Core.Dtos.BasketItem;
using Shop.Domain.Aggregates.Baskets;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketFormDto
{
    public List<BasketItemFormDto> BasketItems { get; set; }

    public Guid? Id { get; set; }

    public static Expression<Func<BasketAggregate, BasketFormDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemFormDto.Map()).ToList(),
        Id = entity.Id,
    };

    public BasketAggregate ToEntity(Guid? userId) => new()
    {
        BasketItems = BasketItems.Select(x => x.ToEntity()).ToList(),
        Id = Id ?? Guid.Empty,
        UserId = userId
    };
}