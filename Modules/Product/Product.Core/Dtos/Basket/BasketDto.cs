using Product.Core.Dtos.BasketItem;
using Product.Domain.Entities;

namespace Product.Core.Dtos.Basket;

public class BasketDto
{
    public BasketDto(BasketEntity entity)
    {
        BasketItems = entity.BasketItems.Select(x => new BasketItemDto(x)).ToList();
        Id = entity.Id;
    }

    public List<BasketItemDto> BasketItems { get; set; }

    public Guid Id { get; set; }
}