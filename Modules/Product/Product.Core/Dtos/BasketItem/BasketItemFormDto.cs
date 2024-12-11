using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using System.Linq.Expressions;

namespace Product.Core.Dtos.BasketItem;

public class BasketItemFormDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public static Expression<Func<BasketItemEntity, BasketItemFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        Quantity = entity.Quantity,
    };

    public BasketItemEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        ProductId = ProductId,
        Quantity = Quantity,
    };
}

public class BasketItemFormValidator : AbstractValidator<BasketItemFormDto>
{
    public BasketItemFormValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
                .ErrorResponse(ErrorMessage.ValueWasEmpty);

        RuleFor(x => x.Quantity)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}