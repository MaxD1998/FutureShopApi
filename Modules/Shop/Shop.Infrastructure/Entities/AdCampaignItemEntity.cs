using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class AdCampaignItemEntity : BaseEntity, IUpdate<AdCampaignItemEntity>
{
    public Guid AdCampaignId { get; set; }

    public string FileId { get; set; }

    public string Lang { get; set; }

    public int Position { get; set; }

    #region Related Data

    public AdCampaignEntity AdCampaign { get; set; }

    #endregion Related Data

    public void Update(AdCampaignItemEntity entity)
    {
        Position = entity.Position;
    }
}