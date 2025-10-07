﻿using Shop.Core.Dtos.AdCampaign.AdCampaignItem;
using Shop.Core.Dtos.AdCampaign.AdCampaignProduct;
using Shop.Infrastructure.Entities.AdCampaigns;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignRequestFormDto
{
    public List<AdCampaignItemFormDto> AdCampaignItems { get; set; }

    public List<AdCampaignProductFormDto> AdCampaignProducts { get; set; }

    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public AdCampaignEntity ToEntity() => new()
    {
        AdCampaignItems = AdCampaignItems.GroupBy(x => x.Lang).SelectMany(x => x.Select((item, index) => item.ToEntity(index))).ToList(),
        AdCampaignProducts = AdCampaignProducts.Select(x => x.ToEntity()).ToList(),
        End = End,
        IsActive = IsActive,
        Name = Name,
        Start = Start,
    };
}