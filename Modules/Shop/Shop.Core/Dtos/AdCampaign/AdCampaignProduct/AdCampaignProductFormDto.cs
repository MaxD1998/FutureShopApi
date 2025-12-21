using Shop.Domain.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign.AdCampaignProduct;

public class AdCampaignProductFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public static Expression<Func<AdCampaignProductEntity, AdCampaignProductFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        ProductName = entity.Product.Name,
    };

    public AdCampaignProductEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId,
    };
}