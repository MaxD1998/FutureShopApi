using Shop.Domain.Aggregates.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductShopListDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public static Expression<Func<ProductAggregate, ProductShopListDto>> Map(string lang, Guid? userId, Guid? favouriteId)
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            FileId = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(x => x.FileId).FirstOrDefault(),
            Id = entity.Id,
            IsInPurchaseList = entity.PurchaseListItems.Any(x => x.PurchaseList.UserId != null && x.PurchaseList.UserId == userId || x.PurchaseListId == favouriteId),
            Name = entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
            Price = entity.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && !x.End.HasValue || utcNow < x.End).Select(x => x.Price).FirstOrDefault(),
        };
    }
}