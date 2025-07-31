using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.AdCampaigns.Entities;

namespace Shop.Domain.Aggregates.AdCampaigns;

public class AdCampaignAggregate : BaseEntity, IUpdate<AdCampaignAggregate>
{
    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    #region Related Data

    public ICollection<AdCampaignItemEntity> AdCampaignItems { get; set; }

    #endregion Related Data

    public void Update(AdCampaignAggregate entity)
    {
        End = entity.End;
        IsActive = entity.IsActive;
        Name = entity.Name;
        Start = entity.Start;

        AdCampaignItems.UpdateEntities(entity.AdCampaignItems);
    }
}