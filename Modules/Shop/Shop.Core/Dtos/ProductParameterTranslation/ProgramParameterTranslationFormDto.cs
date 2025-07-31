using Shared.Core.Bases;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.ProductParameterTranslation;

public class ProgramParameterTranslationFormDto : BaseTranslationFormDto<ProductParameterTranslationEntity, ProgramParameterTranslationFormDto>
{
    public override ProductParameterTranslationEntity ToEntity() => new(Id ?? Guid.Empty, Lang, Translation);
}