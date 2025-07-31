using Shared.Core.Bases;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.ProductTranslation;

public class ProductTranslationFormDto : BaseTranslationFormDto<ProductTranslationEntity, ProductTranslationFormDto>
{
    public override ProductTranslationEntity ToEntity() => new(Id ?? Guid.Empty, Lang, Translation);
}