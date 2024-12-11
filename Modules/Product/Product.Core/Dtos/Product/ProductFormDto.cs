using FluentValidation;
using Product.Core.Dtos.ProductParameterValue;
using Product.Core.Dtos.ProductPhoto;
using Product.Core.Dtos.ProductTranslation;
using Product.Domain.Entities;
using System.Linq.Expressions;

namespace Product.Core.Dtos.Product;

public class ProductFormDto
{
    public string Description { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductParameterValueFormDto> ProductParameterValues { get; set; }

    public List<ProductPhotoFormDto> ProductPhotos { get; set; }

    public List<ProductTranslationFormDto> Translations { get; set; }

    public static Expression<Func<ProductEntity, ProductFormDto>> Map() => entity => new()
    {
        Description = entity.Description,
        Name = entity.Name,
        Price = entity.Price,
        ProductBaseId = entity.ProductBaseId,
        ProductParameterValues = entity.ProductParameterValues.AsQueryable().Select(ProductParameterValueFormDto.Map()).ToList(),
        ProductPhotos = entity.ProductPhotos.AsQueryable().OrderBy(x => x.Position).Select(ProductPhotoFormDto.Map()).ToList(),
        Translations = entity.Translations.AsQueryable().Select(ProductTranslationFormDto.Map()).ToList(),
    };

    public ProductEntity ToEntity() => new()
    {
        Description = Description,
        Name = Name,
        Price = Price,
        ProductBaseId = ProductBaseId,
        ProductParameterValues = ProductParameterValues.Select(x => x.ToEntity()).ToList(),
        ProductPhotos = ProductPhotos.Select((x, index) => x.ToEntity(index)).ToList(),
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}

public class ProductFormValidator : AbstractValidator<ProductFormDto>
{
}