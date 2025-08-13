using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;
using Shared.Shared.Helpers;
using Shared.Shared.Interfaces;

namespace Shop.Infrastructure.Entities;

public class BasketEntity : BaseEntity, IUpdate<BasketEntity>, ICloneable<BasketEntity>
{
    public Guid? UserId { get; set; }

    #region Realted Data

    public ICollection<BasketItemEntity> BasketItems { get; set; } = [];

    #endregion Realted Data

    public BasketEntity Clone() => DeepClone.Clone(this);

    public void Update(BasketEntity entity)
    {
        BasketItems.UpdateEntities(entity.BasketItems);
    }
}