using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos.ProductTranslation;

public class ProductTranslationFormDto : BaseTranslationFormDto<ProductTranslationEntity>
{
    public ProductTranslationFormDto()
    {
    }

    public ProductTranslationFormDto(ProductTranslationEntity entity) : base(entity)
    {
    }
}