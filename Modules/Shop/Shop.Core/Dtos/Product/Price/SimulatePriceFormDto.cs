using Shop.Infrastructure.Entities.Products;

namespace Shop.Core.Dtos.Product.Price;

public class SimulatePriceFormDto : PriceFormDto
{
    public int FakeId { get; set; }
    public bool IsNew { get; set; }

    public static new Func<PriceEntity, SimulatePriceFormDto> Map() => entity => new()
    {
        End = entity.End,
        Id = entity.Id,
        Price = entity.Price,
        Start = entity.Start,
    };
}