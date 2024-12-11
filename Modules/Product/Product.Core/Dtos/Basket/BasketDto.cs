using Product.Core.Dtos.BasketItem;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Basket;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; }

    public Guid Id { get; set; }

    public static Expression<Func<BasketEntity, BasketDto>> Map(Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate) => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemDto.Map(isInPurchaseListPredicate)).ToList(),
        Id = entity.Id,
    };
}