using Shop.Core.Dtos.Promotion.PromotionProduct;
using Shop.Domain.Entities.Promotions;
using Shop.Domain.Enums;
using System.Text.Json;

namespace Shop.Core.Dtos.Promotion;

public class PromotionRequestFormDto
{
    public Guid? AdCampaignId { get; set; }

    public string Code { get; set; }

    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public List<PromotionProductFormDto> PromotionProducts { get; set; }

    public DateTime Start { get; set; }

    public PromotionType Type { get; set; }

    public JsonDocument Value { get; set; }

    public PromotionEntity ToEntity() => new()
    {
        AdCampaignId = AdCampaignId,
        Code = Code,
        End = End,
        IsActive = IsActive,
        Name = Name,
        Start = Start,
        Type = Type,
        Value = Value,
        PromotionProducts = PromotionProducts.Select(x => x.ToEntity()).ToList(),
    };
}