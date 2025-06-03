namespace Shop.Core.Dtos.Basket;

public class ImportPurchaseListToBasketDto
{
    public Guid BasketId { get; set; }

    public Guid PurchaseListId { get; set; }
}