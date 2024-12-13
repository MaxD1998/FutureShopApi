using FluentValidation;
using Shared.Core.Bases;
using Shop.Domain.Entities;

namespace Shop.Core.Dtos.ProductTranslation;

public class ProductTranslationFormDto : BaseTranslationFormDto<ProductTranslationEntity, ProductTranslationFormDto>
{
}

public class ProductTranslationFormValidator : AbstractValidator<ProductTranslationFormDto>
{
}