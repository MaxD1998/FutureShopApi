using Shop.Domain.Aggregates.Baskets.Entities;
using Shop.Domain.Aggregates.PurchaseLists.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.BasketItem;

public class BasketItemDto
{
    public Guid Id { get; set; }

    public string ProductFileId { get; set; }

    public Guid ProductId { get; set; }

    public bool ProductIsInPurchaseList { get; set; }

    public string ProductName { get; set; }

    public decimal ProductPrice { get; set; }

    public int Quantity { get; set; }

    public static Expression<Func<BasketItemEntity, BasketItemDto>> Map(Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate)
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            Id = entity.Id,
            ProductFileId = entity.Product.ProductPhotos.AsQueryable().Select(x => x.FileId).FirstOrDefault(),
            ProductId = entity.ProductId,
            ProductIsInPurchaseList = entity.Product.PurchaseListItems.AsQueryable().Any(isInPurchaseListPredicate),
            ProductName = entity.Product.Name,
            ProductPrice = entity.Product.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
            Quantity = entity.Quantity,
        };
    }
}