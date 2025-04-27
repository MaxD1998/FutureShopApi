using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignDto
{
    public IEnumerable<string> FileIds { get; set; }

    public static Expression<Func<AdCampaignEntity, AdCampaignDto>> Map() => entity => new()
    {
        FileIds = entity.AdCampaignItems.AsQueryable().Select(x => x.FileId).ToList(),
    };

    public static AdCampaignDto Merge(List<AdCampaignDto> list) => new()
    {
        FileIds = list.SelectMany(x => x.FileIds).ToList(),
    };
}