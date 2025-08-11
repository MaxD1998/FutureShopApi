using Shop.Domain.Aggregates.Products;
using Shop.Domain.Aggregates.PurchaseLists.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductBasketItemDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public static Expression<Func<ProductAggregate, ProductBasketItemDto>> Map(Expression<Func<PurchaseListItemEntity, bool>> isInPurchaseListPredicate)
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            Id = entity.Id,
            FileId = entity.ProductPhotos.AsQueryable().Select(x => x.FileId).FirstOrDefault(),
            IsInPurchaseList = entity.PurchaseListItems.AsQueryable().Any(isInPurchaseListPredicate),
            Name = entity.Name,
            Price = entity.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
        };
    }
}