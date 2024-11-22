using Product.Domain.Entities;

namespace Product.Core.Dtos.PurchaseListItem;

public class PurchaseListItemDto
{
    public PurchaseListItemDto(PurchaseListItemEntity entity)
    {
        Id = entity.Id;
        ProductFileId = entity.Product?.ProductPhotos.FirstOrDefault()?.FileId;
        ProductId = entity.ProductId;
        ProductName = entity.Product?.Name;
        PurchaseListId = entity.PurchaseListId;
    }

    public Guid Id { get; set; }

    public string ProductFileId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public Guid PurchaseListId { get; set; }
}