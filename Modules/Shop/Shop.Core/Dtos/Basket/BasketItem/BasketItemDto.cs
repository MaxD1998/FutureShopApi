using Shop.Domain.Entities.Baskets;
using Shop.Domain.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Basket.BasketItem;

public class BasketItemDto
{
    public Guid Id { get; set; }

    public BasketItemProductDto Product { get; set; }

    public int Quantity { get; set; }

    public static Expression<Func<BasketItemEntity, BasketItemDto>> Map(Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate)
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            Id = entity.Id,
            Product = new BasketItemProductDto()
            {
                FileId = entity.Product.ProductPhotos.AsQueryable().Select(x => x.FileId).FirstOrDefault(),
                Id = entity.ProductId,
                IsInPurchaseList = entity.Product.PurchaseListItems.AsQueryable().Any(isInPurchaseListPredicate),
                Name = entity.Product.Name,
                OriginalPrice = entity.Product.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
                Price = entity.Product.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
            },
            Quantity = entity.Quantity,
        };
    }
}