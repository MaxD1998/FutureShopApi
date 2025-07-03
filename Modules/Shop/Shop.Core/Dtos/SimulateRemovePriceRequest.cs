using Shop.Core.Dtos.Price;

namespace Shop.Core.Dtos;

public class SimulateRemovePriceRequest
{
    public List<PriceFormDto> NewCollection { get; set; }

    public List<PriceFormDto> OldCollection { get; set; }

    public Guid? ProductId { get; set; }
}