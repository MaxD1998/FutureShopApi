﻿using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductListDto
{
    public string FilledParameters { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public static Expression<Func<ProductEntity, ProductListDto>> Map() => entity => new()
    {
        FilledParameters = $"{entity.ProductParameterValues.Count}/{entity.ProductBase.ProductParameters.Count}",
        Id = entity.Id,
        Name = entity.Name,
        Price = entity.Price,
    };
}