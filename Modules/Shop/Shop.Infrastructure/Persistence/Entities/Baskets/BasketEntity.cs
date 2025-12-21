using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;
using Shared.Shared.Helpers;
using Shared.Shared.Interfaces;

namespace Shop.Infrastructure.Persistence.Entities.Baskets;

public class BasketEntity : BaseEntity, IUpdate<BasketEntity>, ICloneable<BasketEntity>, IEntityValidation
{
    public Guid? UserId { get; set; }

    #region Realted Data

    public ICollection<BasketItemEntity> BasketItems { get; set; } = [];

    #endregion Realted Data

    #region Methods

    public BasketEntity Clone() => DeepClone.Clone(this);

    public void Update(BasketEntity entity)
    {
        BasketItems.UpdateEntities(entity.BasketItems);
    }

    public void Validate()
    {
        BasketItems.ValidateEntities();
    }

    #endregion Methods
}