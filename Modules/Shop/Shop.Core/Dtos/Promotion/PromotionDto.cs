using Shop.Core.Dtos.Product;

namespace Shop.Core.Dtos.Promotion;

public class PromotionDto
{
    public string Code { get; set; }

    public Guid Id { get; set; }

    public List<ProductShopListDto> Products { get; set; }
}