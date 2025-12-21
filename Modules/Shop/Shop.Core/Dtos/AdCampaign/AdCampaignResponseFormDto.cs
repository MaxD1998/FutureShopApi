using Shop.Core.Dtos.AdCampaign.AdCampaignItem;
using Shop.Core.Dtos.AdCampaign.AdCampaignProduct;
using Shop.Infrastructure.Persistence.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignResponseFormDto : AdCampaignRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<AdCampaignEntity, AdCampaignResponseFormDto>> Map() => entity => new()
    {
        AdCampaignItems = entity.AdCampaignItems.AsQueryable().Select(AdCampaignItemFormDto.Map()).ToList(),
        AdCampaignProducts = entity.AdCampaignProducts.AsQueryable().Select(AdCampaignProductFormDto.Map()).ToList(),
        End = entity.End,
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Start = entity.Start,
    };
}