using Shop.Core.Dtos.AdCampaignItem;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignRequestFormDto
{
    public List<AdCampaignItemFormDto> AdCampaignItems { get; set; }

    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public AdCampaignEntity ToEntity() => new()
    {
        AdCampaignItems = AdCampaignItems.GroupBy(x => x.Lang).SelectMany(x => x.Select((item, index) => item.ToEntity(index))).ToList(),
        End = End,
        IsActive = IsActive,
        Name = Name,
        Start = Start,
    };
}