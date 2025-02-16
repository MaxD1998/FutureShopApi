using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductDto
{
    public IEnumerable<string> FileIds { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<IdNameValueDto> ProductParameters { get; set; }

    public static Expression<Func<ProductEntity, ProductDto>> Map(string lang, Guid? userId, Guid? favouriteId) => entity => new()
    {
        FileIds = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(x => x.FileId).ToList(),
        Id = entity.Id,
        IsInPurchaseList = entity.PurchaseListItems.Any(x => x.PurchaseList.UserId != null && x.PurchaseList.UserId == userId || x.PurchaseListId == favouriteId),
        Name = entity.Translations.AsQueryable().Where(x => x.Lang == lang).Select(x => x.Translation).FirstOrDefault() ?? entity.Name,
        Price = entity.Price,
        ProductParameters = entity.ProductParameterValues.AsQueryable().Select(IdNameValueDto.MapFromProductParameterValue(lang)).ToList(),
    };
}