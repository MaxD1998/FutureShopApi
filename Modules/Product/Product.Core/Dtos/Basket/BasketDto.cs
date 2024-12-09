using Product.Core.Dtos.BasketItem;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Basket;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; }

    public Guid Id { get; set; }

    public static Expression<Func<BasketEntity, BasketDto>> Map(Guid? favouriteId) => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemDto.Map(favouriteId)).ToList(),
        Id = entity.Id,
    };
}