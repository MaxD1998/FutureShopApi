using Shop.Core.Dtos.BasketItem;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketFormDto
{
    public List<BasketItemFormDto> BasketItems { get; set; }

    public Guid? Id { get; set; }

    public static Expression<Func<BasketEntity, BasketFormDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemFormDto.Map()).ToList(),
        Id = entity.Id,
    };

    public BasketEntity ToEntity() => new()
    {
        BasketItems = BasketItems.Select(x => x.ToEntity()).ToList(),
        Id = Id ?? Guid.Empty
    };
}