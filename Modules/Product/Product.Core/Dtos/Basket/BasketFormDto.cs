﻿using FluentValidation;
using Product.Core.Dtos.BasketItem;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Basket;

public class BasketFormDto
{
    public List<BasketItemFormDto> BasketItems { get; set; }

    public Guid? Id { get; set; }

    public static Expression<Func<BasketEntity, BasketFormDto>> Map() => entity => new()
    {
        BasketItems = entity.BasketItems.AsQueryable().Select(BasketItemFormDto.Map()).ToList(),
        Id = entity.Id,
    };

    public BasketEntity ToEntity() => new()
    {
        BasketItems = BasketItems.Select(x => x.ToEntity()).ToList(),
        Id = Id ?? Guid.Empty
    };
}

public class BasketFormValidator : AbstractValidator<BasketFormDto>
{
    public BasketFormValidator()
    {
        RuleForEach(x => x.BasketItems)
            .SetValidator(new BasketItemFormValidator());
    }
}