using Shop.Core.Dtos.Promotion.PromotionProduct;
using Shop.Domain.Entities.Promotions;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Promotion;

public class PromotionResponseFormDto : PromotionRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<PromotionEntity, PromotionResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        End = entity.End,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Start = entity.Start,
        Type = entity.Type,
        Value = entity.Value,
        PromotionProducts = entity.PromotionProducts.AsQueryable().Select(PromotionProductFormDto.Map()).OrderBy(x => x.ProductName).ToList(),
    };
}