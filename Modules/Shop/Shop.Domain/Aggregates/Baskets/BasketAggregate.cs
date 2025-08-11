using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shared.Shared.Interfaces;
using Shop.Domain.Aggregates.Baskets.Comparers;
using Shop.Domain.Aggregates.Baskets.Entities;

namespace Shop.Domain.Aggregates.Baskets;

public class BasketAggregate : BaseEntity, IUpdate<BasketAggregate>, ICloneable<BasketAggregate>
{
    private HashSet<BasketItemEntity> _basketItems = BasketItemEntityComparer.CreateSet();

    public BasketAggregate(Guid? userId, IEnumerable<BasketItemEntity> basketItems)
    {
        SetUserId(userId);
        SetBasketItems(basketItems);
    }

    private BasketAggregate()
    {
    }

    public Guid? UserId { get; private set; }

    #region Realted Data

    public IReadOnlyCollection<BasketItemEntity> BasketItems => _basketItems;

    #endregion Realted Data

    #region Setter

    private void SetBasketItems(IEnumerable<BasketItemEntity> basketItems)
    {
        _basketItems = BasketItemEntityComparer.CreateSet(basketItems);
    }

    private void SetUserId(Guid? userId)
    {
        UserId = userId;
    }

    #endregion Setter

    #region Methods

    public void AddBasketItem(BasketItemEntity basketItem)
    {
        _basketItems.Add(basketItem);
    }

    public BasketAggregate Clone() => new()
    {
        Id = Id,
        CreateTime = CreateTime,
        ModifyTime = ModifyTime,
        UserId = UserId,

        _basketItems = BasketItemEntityComparer.CreateSet(BasketItems),
    };

    public void Update(BasketAggregate entity)
    {
        _basketItems.UpdateEntities(entity.BasketItems);
    }

    #endregion Methods
}