using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class PurchaseListEntity : BaseEntity, IUpdate<PurchaseListEntity>
{
    public bool IsFavourite { get; set; }

    public string Name { get; set; }

    public Guid? UserId { get; set; }

    #region Related Data

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; set; } = [];

    #endregion Related Data

    public void Update(PurchaseListEntity entity)
    {
        IsFavourite = entity.IsFavourite;
        Name = entity.Name;
        PurchaseListItems.UpdateEntities(entity.PurchaseListItems);
    }
}