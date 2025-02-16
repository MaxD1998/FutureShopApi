using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public static Expression<Func<ProductBaseEntity, ProductBaseFormDto>> Map() => entity => new()
    {
        CategoryId = entity.CategoryId,
        Name = entity.Name,
    };

    public ProductBaseEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
    };
}

public class ProductBaseValidator : AbstractValidator<ProductBaseFormDto>
{
    public ProductBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}