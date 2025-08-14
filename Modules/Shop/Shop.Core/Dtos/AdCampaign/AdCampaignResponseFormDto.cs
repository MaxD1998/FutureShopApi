using Shop.Core.Dtos.AdCampaignItem;
using Shop.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignResponseFormDto : AdCampaignRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<AdCampaignEntity, AdCampaignResponseFormDto>> Map() => entity => new()
    {
        AdCampaignItems = entity.AdCampaignItems.AsQueryable().Select(AdCampaignItemFormDto.Map()).ToList(),
        End = entity.End,
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Start = entity.Start,
    };
}