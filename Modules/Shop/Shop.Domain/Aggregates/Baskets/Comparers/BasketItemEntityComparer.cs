using Shop.Domain.Aggregates.Baskets.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Domain.Aggregates.Baskets.Comparers;

public class BasketItemEntityComparer : IEqualityComparer<BasketItemEntity>
{
    public static HashSet<BasketItemEntity> CreateSet() => new HashSet<BasketItemEntity>(new BasketItemEntityComparer());

    public static HashSet<BasketItemEntity> CreateSet(IEnumerable<BasketItemEntity> basketItems) => basketItems.ToHashSet(new BasketItemEntityComparer());

    public bool Equals(BasketItemEntity x, BasketItemEntity y) => x.ProductId == y.ProductId;

    public int GetHashCode([DisallowNull] BasketItemEntity obj) => HashCode.Combine(obj.ProductId);
}