using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.PurchaseLists.Entities;

namespace Shop.Domain.Aggregates.PurchaseLists;

public class PurchaseListAggregate : BaseEntity, IUpdate<PurchaseListAggregate>
{
    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public Guid? UserId { get; set; }

    #region Related Data

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    #endregion Related Data

    public void Update(PurchaseListAggregate entity)
    {
        IsFavourite = entity.IsFavourite;
        Name = entity.Name;
        PurchaseListItems.UpdateEntities(entity.PurchaseListItems);
    }
}