using Shop.Core.Dtos.Basket.BasketItem;
using Shop.Infrastructure.Entities.Baskets;
using Shop.Infrastructure.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; } = [];

    public Guid Id { get; set; }

    public static Expression<Func<BasketEntity, BasketDto>> Map(Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate) => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemDto.Map(isInPurchaseListPredicate)).ToList(),
        Id = entity.Id,
    };
}