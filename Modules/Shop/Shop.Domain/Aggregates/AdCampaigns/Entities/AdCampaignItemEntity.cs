using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Aggregates.AdCampaigns.Entities;

public class AdCampaignItemEntity : BaseEntity, IUpdate<AdCampaignItemEntity>
{
    public Guid AdCampaignId { get; set; }

    public string FileId { get; set; }

    public string Lang { get; set; }

    public int Position { get; set; }

    public void Update(AdCampaignItemEntity entity)
    {
        Position = entity.Position;
    }
}