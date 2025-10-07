using Shop.Core.Interfaces;

namespace Shop.Core.Dtos.Basket.BasketItem;

public class BasketItemProductDto : IProductPrice
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal Price { get; set; }
}