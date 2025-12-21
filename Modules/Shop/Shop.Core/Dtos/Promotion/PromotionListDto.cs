using Shop.Domain.Entities.Promotions;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Promotion;

public class PromotionListDto
{
    public string Code { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public int QuantityOfPromotionProducts { get; set; }

    public static Expression<Func<PromotionEntity, PromotionListDto>> Map() => entity => new()
    {
        Code = entity.Code,
        Id = entity.Id,
        Name = entity.Name,
        QuantityOfPromotionProducts = entity.PromotionProducts.Count
    };
}