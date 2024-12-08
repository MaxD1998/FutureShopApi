using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.PurchaseList;

public class ImportBasketToPurchaseListDto
{
    public Guid BasketId { get; set; }

    public string Name { get; set; }
}

public class ImportBasketToPurchaseListValidator : AbstractValidator<ImportBasketToPurchaseListDto>
{
    public ImportBasketToPurchaseListValidator()
    {
        RuleFor(x => x.BasketId)
            .NotEqual(Guid.Empty)
                .ErrorResponse(ErrorMessage.ValueWasEmpty);

        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}