using Shared.Core.Bases;
using Shop.Domain.Aggregates.Categories.Entities;

namespace Shop.Core.Dtos.CategoryTranslation;

public class CategoryTranslationFormDto : BaseTranslationFormDto<CategoryTranslationEntity, CategoryTranslationFormDto>
{
    public override CategoryTranslationEntity ToEntity() => new(Id ?? Guid.Empty, Lang, Translation);
}