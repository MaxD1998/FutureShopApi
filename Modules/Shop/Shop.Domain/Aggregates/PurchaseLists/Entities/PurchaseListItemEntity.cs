using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.PurchaseLists.Entities;

public class PurchaseListItemEntity : BaseEntity, IUpdate<PurchaseListItemEntity>
{
    public Guid ProductId { get; set; }

    public Guid PurchaseListId { get; set; }

    #region Related Data

    public ProductAggregate Product { get; set; }

    public PurchaseListAggregate PurchaseList { get; set; }

    #endregion Related Data

    public void Update(PurchaseListItemEntity entity)
    {
        ProductId = entity.ProductId;
    }
}