using FluentValidation;

namespace Product.Core.Dtos.ProductParameterTranslation;

public class ProgramParameterTranslationValidator : AbstractValidator<ProgramParameterTranslationFormDto>
{
    public ProgramParameterTranslationValidator()
    {
    }
}