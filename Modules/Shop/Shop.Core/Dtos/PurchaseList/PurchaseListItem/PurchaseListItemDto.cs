using Shop.Domain.Entities.PurchaseLists;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.PurchaseList.PurchaseListItem;

public class PurchaseListItemDto
{
    public Guid Id { get; set; }

    public string ProductFileId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public Guid PurchaseListId { get; set; }

    public static Expression<Func<PurchaseListItemEntity, PurchaseListItemDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductFileId = entity.Product.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(x => x.FileId).FirstOrDefault(),
        ProductId = entity.ProductId,
        ProductName = entity.Product.Name,
        PurchaseListId = entity.PurchaseListId,
    };
}