using Shop.Core.Dtos.AdCampaignItem;
using Shop.Domain.Aggregates.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignFormDto
{
    public List<AdCampaignItemFormDto> AdCampaignItems { get; set; }

    public DateTime End { get; set; }

    public Guid? Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public static Expression<Func<AdCampaignAggregate, AdCampaignFormDto>> Map() => entity => new()
    {
        AdCampaignItems = entity.AdCampaignItems.AsQueryable().Select(AdCampaignItemFormDto.Map()).ToList(),
        End = entity.End,
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Start = entity.Start,
    };

    public AdCampaignAggregate ToEntity() => new()
    {
        AdCampaignItems = AdCampaignItems.GroupBy(x => x.Lang).SelectMany(x => x.Select((item, index) => item.ToEntity(index))).ToList(),
        End = End,
        Id = Id ?? Guid.Empty,
        IsActive = IsActive,
        Name = Name,
        Start = Start,
    };
}