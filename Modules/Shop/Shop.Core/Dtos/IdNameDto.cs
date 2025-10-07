using Shared.Core.Bases;
using Shop.Infrastructure.Entities.AdCampaigns;
using Shop.Infrastructure.Entities.Categories;
using Shop.Infrastructure.Entities.ProductBases;
using Shop.Infrastructure.Entities.Products;
using Shop.Infrastructure.Entities.Promotions;
using System.Linq.Expressions;

namespace Shop.Core.Dtos;

public class IdNameDto : BaseIdNameDto
{
    public static Expression<Func<AdCampaignEntity, IdNameDto>> MapFromAdCampaign() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Expression<Func<CategoryEntity, IdNameDto>> MapFromCategory() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Expression<Func<ProductEntity, IdNameDto>> MapFromProduct() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Expression<Func<ProductBaseEntity, IdNameDto>> MapFromProductBase() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Expression<Func<PromotionEntity, IdNameDto>> MapFromPromotion() => entity => new IdNameDto
    {
        Id = entity.Id,
        Name = entity.Name
    };
}