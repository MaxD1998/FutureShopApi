using FluentValidation;
using Shared.Core.Bases;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.ProductParameterTranslation;

public class ProgramParameterTranslationFormDto : BaseTranslationFormDto<ProductParameterTranslationEntity, ProgramParameterTranslationFormDto>
{
}

public class ProgramParameterTranslationFormValidator : AbstractValidator<ProgramParameterTranslationFormDto>
{
    public ProgramParameterTranslationFormValidator()
    {
    }
}