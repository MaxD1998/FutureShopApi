using Shop.Domain.Aggregates.PurchaseLists.Entities;

namespace Shop.Core.Dtos.PurchaseListItem;

public class PurchaseListItemFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public PurchaseListItemEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId,
    };
}