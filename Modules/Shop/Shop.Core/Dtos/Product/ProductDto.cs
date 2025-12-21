using Shop.Core.Interfaces;
using Shop.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductDto : IProductPrice
{
    public IEnumerable<string> FileIds { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal Price { get; set; }

    public List<IdNameValueDto> ProductParameters { get; set; }

    public double Rating { get; set; }

    public int ReviewCount { get; set; }

    public bool UserWasReviewer { get; set; }

    public static Expression<Func<ProductEntity, ProductDto>> Map(string lang, Guid? userId, Guid? favouriteId)
    {
        var utcNow = DateTime.UtcNow;

        return entity => new()
        {
            FileIds = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(x => x.FileId).ToList(),
            Id = entity.Id,
            IsInPurchaseList = entity.PurchaseListItems.AsQueryable().Any(x => x.PurchaseList.UserId != null && x.PurchaseList.UserId == userId || x.PurchaseListId == favouriteId),
            Name = entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
            OriginalPrice = entity.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
            Price = entity.Prices.AsQueryable().Where(x => (!x.Start.HasValue || x.Start <= utcNow) && (!x.End.HasValue || utcNow < x.End)).Select(x => x.Price).FirstOrDefault(),
            ProductParameters = entity.ProductParameterValues.AsQueryable().Select(IdNameValueDto.MapFromProductParameterValue(lang)).ToList(),
            Rating = entity.ProductReviews.Any() ? entity.ProductReviews.Average(x => x.Rating) : 0,
            ReviewCount = entity.ProductReviews.Count(),
            UserWasReviewer = userId.HasValue ? entity.ProductReviews.AsQueryable().Any(x => x.UserId == userId) : false
        };
    }
}