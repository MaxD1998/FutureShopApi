using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shared.Shared.Helpers;
using Shared.Shared.Interfaces;
using Shop.Domain.Aggregates.Baskets.Entities;

namespace Shop.Domain.Aggregates.Baskets;

public class BasketAggregate : BaseEntity, IUpdate<BasketAggregate>, ICloneable<BasketAggregate>
{
    public Guid? UserId { get; set; }

    #region Realted Data

    public ICollection<BasketItemEntity> BasketItems { get; set; } = [];

    #endregion Realted Data

    public BasketAggregate Clone() => DeepClone.Clone(this);

    public void Update(BasketAggregate entity)
    {
        BasketItems.UpdateEntities(entity.BasketItems);
    }
}