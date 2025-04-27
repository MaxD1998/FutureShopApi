using FluentValidation;
using Shop.Core.Dtos.ProductParameterValue;
using Shop.Core.Dtos.ProductTranslation;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductFormDto
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductParameterValueFormDto> ProductParameterValues { get; set; }

    public List<ProductTranslationFormDto> Translations { get; set; }

    public static Expression<Func<ProductEntity, ProductFormDto>> Map() => entity => new()
    {
        IsActive = entity.IsActive,
        Name = entity.Name,
        Price = entity.Price,
        ProductBaseId = entity.ProductBaseId,
        ProductParameterValues = entity.ProductParameterValues.AsQueryable().Select(ProductParameterValueFormDto.Map()).ToList(),
        Translations = entity.Translations.AsQueryable().Select(ProductTranslationFormDto.Map()).ToList(),
    };

    public ProductEntity ToEntity() => new()
    {
        IsActive = IsActive,
        Name = Name,
        Price = Price,
        ProductBaseId = ProductBaseId,
        ProductParameterValues = ProductParameterValues.Select(x => x.ToEntity()).ToList(),
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}

public class ProductFormValidator : AbstractValidator<ProductFormDto>
{
}