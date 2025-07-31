using Shared.Domain.Bases;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.AdCampaigns;

namespace Shop.Domain.Aggregates.AdCampaigns.Entities;

public class AdCampaignItemEntity : BaseEntity, IUpdate<AdCampaignItemEntity>
{
    public Guid AdCampaignId { get; set; }

    public string FileId { get; set; }

    public string Lang { get; set; }

    public int Position { get; set; }

    #region Related Data

    public AdCampaignAggregate AdCampaign { get; set; }

    #endregion Related Data

    public void Update(AdCampaignItemEntity entity)
    {
        Position = entity.Position;
    }
}