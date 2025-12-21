using Shared.Core.Bases;
using Shop.Domain.Entities.AdCampaigns;
using Shop.Domain.Entities.Categories;
using Shop.Domain.Entities.ProductBases;
using Shop.Domain.Entities.Products;
using Shop.Domain.Entities.Promotions;
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