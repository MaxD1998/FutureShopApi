using Shop.Domain.Aggregates.AdCampaigns.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Domain.Aggregates.AdCampaigns.Comparers;

public class AdCampaignItemEntityComparer : IEqualityComparer<AdCampaignItemEntity>
{
    public static HashSet<AdCampaignItemEntity> CreateSet() => new HashSet<AdCampaignItemEntity>(new AdCampaignItemEntityComparer());

    public static HashSet<AdCampaignItemEntity> CreateSet(IEnumerable<AdCampaignItemEntity> adCampaignItems) => adCampaignItems.ToHashSet(new AdCampaignItemEntityComparer());

    public bool Equals(AdCampaignItemEntity x, AdCampaignItemEntity y) => x.FileId == y.FileId && x.Lang == y.Lang;

    public int GetHashCode([DisallowNull] AdCampaignItemEntity obj) => HashCode.Combine(obj.FileId, obj.Lang);
}