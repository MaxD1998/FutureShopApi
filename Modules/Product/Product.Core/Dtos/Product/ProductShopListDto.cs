using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Product;

public class ProductShopListDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public static Expression<Func<ProductEntity, ProductShopListDto>> Map(string lang, Guid? userId, Guid? favouriteId) => entity => new()
    {
        FileId = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(x => x.FileId).FirstOrDefault(),
        Id = entity.Id,
        IsInPurchaseList = entity.PurchaseListItems.Any(x => (x.PurchaseList.UserId != null && x.PurchaseList.UserId == userId) || x.PurchaseListId == favouriteId),
        Name = entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
        Price = entity.Price,
    };
}