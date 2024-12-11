using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos.ProductParameterTranslation;

public class ProgramParameterTranslationFormDto : BaseTranslationFormDto<ProductParameterTranslationEntity, ProgramParameterTranslationFormDto>
{
}

public class ProgramParameterTranslationFormValidator : AbstractValidator<ProgramParameterTranslationFormDto>
{
    public ProgramParameterTranslationFormValidator()
    {
    }
}