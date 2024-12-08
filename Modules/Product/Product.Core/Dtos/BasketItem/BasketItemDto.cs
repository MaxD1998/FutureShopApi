using Product.Domain.Entities;

namespace Product.Core.Dtos.BasketItem;

public class BasketItemDto
{
    public BasketItemDto(BasketItemEntity entity)
    {
        Id = entity.Id;
        ProductFileId = entity.Product.ProductPhotos.FirstOrDefault()?.FileId;
        ProductId = entity.ProductId;
        ProductIsInPurchaseList = entity.Product.PurchaseListItems.Any();
        ProductName = entity.Product.Name;
        ProductPrice = entity.Product.Price;
        Quantity = entity.Quantity;
    }

    public Guid Id { get; set; }

    public string ProductFileId { get; set; }

    public Guid ProductId { get; set; }

    public bool ProductIsInPurchaseList { get; set; }

    public string ProductName { get; set; }

    public decimal ProductPrice { get; set; }

    public int Quantity { get; set; }
}