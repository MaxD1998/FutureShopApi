using FluentValidation;
using Product.Core.Dtos.ProductParameterTranslation;
using Product.Domain.Entities;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using System.Linq.Expressions;

namespace Product.Core.Dtos.ProductParameter;

public class ProductParameterFormDto
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public List<ProgramParameterTranslationFormDto> Translations { get; set; }

    public static Expression<Func<ProductParameterEntity, ProductParameterFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Translations = entity.Translations.AsQueryable().Select(ProgramParameterTranslationFormDto.Map()).ToList(),
    };

    public ProductParameterEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Name = Name,
        Translations = Translations.Select(x => x.ToEntity()).ToList()
    };
}

public class ProductParameterFormValidator : AbstractValidator<ProductParameterFormDto>
{
    public ProductParameterFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}