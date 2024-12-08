using FluentValidation;
using Product.Core.Dtos.BasketItem;
using Product.Domain.Entities;

namespace Product.Core.Dtos.Basket;

public class BasketFormDto
{
    public BasketFormDto()
    {
    }

    public BasketFormDto(BasketEntity entity)
    {
        BasketItems = entity.BasketItems.Select(x => new BasketItemFormDto(x)).ToList();
        Id = entity.Id;
    }

    public List<BasketItemFormDto> BasketItems { get; set; }

    public Guid? Id { get; set; }

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