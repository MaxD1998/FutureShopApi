using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Bases;

namespace Product.Core.Dtos.ProductTranslation;

public class ProductTranslationFormDto : BaseTranslationFormDto<ProductTranslationEntity, ProductTranslationFormDto>
{
}

public class ProductTranslationFormValidator : AbstractValidator<ProductTranslationFormDto>
{
}