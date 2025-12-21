using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Promotion;
using Shop.Domain.Entities.AdCampaigns;
using Shop.Domain.Enums;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaign;

public class AdCampaignDto : IdFileIdDto
{
    public List<ProductShopListDto> Products { get; set; }

    public PromotionDto Promotion { get; set; }

    public AdCampaignType Type { get; set; }

    public static Expression<Func<AdCampaignEntity, AdCampaignDto>> Map(string lang, Guid? userId, Guid? favouriteId) => entity => new AdCampaignDto
    {
        Id = entity.Id,
        FileId = entity.AdCampaignItems.AsQueryable().Where(x => x.Lang == lang).Select(x => x.FileId).FirstOrDefault(),
        Products = entity.AdCampaignProducts
            .AsQueryable()
            .Select(x => x.Product)
            .Select(ProductShopListDto.Map(lang, userId, favouriteId))
            .ToList(),
        Promotion = new PromotionDto
        {
            Id = entity.Id,
            Code = entity.Promotion.Code,
            Products = entity.Promotion.PromotionProducts
                .AsQueryable()
                .Select(x => x.Product)
                .Select(ProductShopListDto.Map(lang, userId, favouriteId))
                .ToList(),
        },
    };
}