﻿using Shop.Core.Dtos.Price;

namespace Shop.Core.Dtos;

public class SimulateRemovePriceRequestDto
{
    public List<SimulatePriceFormDto> NewCollection { get; set; }

    public List<SimulatePriceFormDto> OldCollection { get; set; }

    public Guid? ProductId { get; set; }
}