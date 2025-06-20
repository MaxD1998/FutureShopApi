﻿using Shop.Core.Dtos.ProductPhoto;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.Product;

public class ProductEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductPhotoEventDto> ProductPhotos { get; set; }

    public ProductEntity Map() => new()
    {
        ExternalId = Id,
        Name = Name,
        ProductPhotos = ProductPhotos.Select(x => x.Map()).ToList(),
    };
}