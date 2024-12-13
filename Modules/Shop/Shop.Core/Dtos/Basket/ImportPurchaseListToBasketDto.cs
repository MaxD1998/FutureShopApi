using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Shop.Core.Dtos.Basket;

public class ImportPurchaseListToBasketDto
{
    public Guid BasketId { get; set; }

    public Guid PurchaseListId { get; set; }
}

public class ImportPurchaseListToBasketValidator : AbstractValidator<ImportPurchaseListToBasketDto>
{
    public ImportPurchaseListToBasketValidator()
    {
        RuleFor(x => x.BasketId)
            .NotEqual(Guid.Empty)
                .ErrorResponse(ErrorMessage.ValueWasEmpty);

        RuleFor(x => x.PurchaseListId)
            .NotEqual(Guid.Empty)
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}