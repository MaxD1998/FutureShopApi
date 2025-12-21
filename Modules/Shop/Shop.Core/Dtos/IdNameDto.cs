using Shared.Core.Bases;
using Shop.Infrastructure.Persistence.Entities.AdCampaigns;
using Shop.Infrastructure.Persistence.Entities.Categories;
using Shop.Infrastructure.Persistence.Entities.ProductBases;
using Shop.Infrastructure.Persistence.Entities.Products;
using Shop.Infrastructure.Persistence.Entities.Promotions;
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