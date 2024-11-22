﻿using Product.Domain.Entities;

namespace Product.Core.Dtos.PurchaseListItem;

public class PurchaseListItemFormDto
{
    public PurchaseListItemFormDto()
    {
    }

    public PurchaseListItemFormDto(PurchaseListItemEntity entity)
    {
    }

    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public PurchaseListItemEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId,
    };
}