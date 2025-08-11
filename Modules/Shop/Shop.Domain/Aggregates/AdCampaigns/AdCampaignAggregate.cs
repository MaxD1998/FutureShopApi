using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shared.Shared.Helpers;
using Shop.Domain.Aggregates.AdCampaigns.Comparers;
using Shop.Domain.Aggregates.AdCampaigns.Entities;
using Shop.Domain.Aggregates.AdCampaigns.Exceptions;

namespace Shop.Domain.Aggregates.AdCampaigns;

public class AdCampaignAggregate : BaseEntity, IUpdate<AdCampaignAggregate>
{
    private HashSet<AdCampaignItemEntity> _adCampaignItems = AdCampaignItemEntityComparer.CreateSet();

    public AdCampaignAggregate(string name, DateTime start, DateTime end, bool isActive, IEnumerable<AdCampaignItemEntity> adCampaignItems)
    {
        adCampaignItems = adCampaignItems ?? [];

        SetName(name);
        SetStartEnd(start, end);
        SetIsActive(isActive, adCampaignItems);
        SetAdCampaignItems(adCampaignItems);
    }

    private AdCampaignAggregate()
    {
    }

    public DateTime End { get; private set; }

    public bool IsActive { get; private set; }

    public string Name { get; private set; }

    public DateTime Start { get; private set; }

    #region Related Data

    public IReadOnlyCollection<AdCampaignItemEntity> AdCampaignItems => _adCampaignItems;

    #endregion Related Data

    #region Setters

    private void SetAdCampaignItems(IEnumerable<AdCampaignItemEntity> adCampaignItems)
    {
        _adCampaignItems = AdCampaignItemEntityComparer.CreateSet(adCampaignItems);
    }

    private void SetIsActive(bool isActive, IEnumerable<AdCampaignItemEntity> adCampaignItems)
    {
        var hasItems = adCampaignItems.Any();

        if (isActive && !hasItems)
            throw new AdCampaignActivationRequiresItemsException();

        IsActive = isActive;
    }

    private void SetName(string name)
    {
        ValidateRequiredLongStringProperty(nameof(Name), name);

        Name = name;
    }

    private void SetStartEnd(DateTime start, DateTime end)
    {
        if (start > end)
            throw new AdCampaignInvalidDateRangeException(start, end);

        if (start < MinimalDate.DateTime)
            throw new AdCampaignStartBeforeAllowedException(start);

        Start = start;
        End = end;
    }

    #endregion Setters

    #region Methods

    public void Update(AdCampaignAggregate entity)
    {
        End = entity.End;
        IsActive = entity.IsActive;
        Name = entity.Name;
        Start = entity.Start;

        _adCampaignItems.UpdateEntities(entity.AdCampaignItems);
    }

    #endregion Methods
}