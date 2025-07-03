using Shop.Core.Dtos.Price;

namespace Shop.Core.Dtos;

public class SimulatePriceRequest
{
    public List<PriceFormDto> Collection { get; set; }

    public PriceFormDto Element { get; set; }

    public Guid? ProductId { get; set; }
}