using Shop.Infrastructure.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignListDto
{
    public int AdCampaignItemQuantity { get; set; }

    public string End { get; set; }

    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public string Start { get; set; }

    public static Expression<Func<AdCampaignEntity, AdCampaignListDto>> Map() => entity => new()
    {
        AdCampaignItemQuantity = entity.AdCampaignItems.AsQueryable().Count(),
        End = entity.End.ToString("dd-MM-yyyy"),
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Start = entity.Start.ToString("dd-MM-yyyy"),
    };
}