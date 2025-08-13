using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class AdCampaignEntity : BaseEntity, IUpdate<AdCampaignEntity>
{
    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    #region Related Data

    public ICollection<AdCampaignItemEntity> AdCampaignItems { get; set; }

    #endregion Related Data

    public void Update(AdCampaignEntity entity)
    {
        End = entity.End;
        IsActive = entity.IsActive;
        Name = entity.Name;
        Start = entity.Start;

        AdCampaignItems.UpdateEntities(entity.AdCampaignItems);
    }
}