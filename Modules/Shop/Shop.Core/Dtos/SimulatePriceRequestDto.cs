using Shop.Core.Dtos.Product.Price;

namespace Shop.Core.Dtos;

public class SimulatePriceRequestDto
{
    public List<SimulatePriceFormDto> Collection { get; set; }

    public SimulatePriceFormDto Element { get; set; }

    public Guid? ProductId { get; set; }
}