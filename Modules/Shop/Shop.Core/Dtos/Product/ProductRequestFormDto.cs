using Shop.Core.Dtos.Price;
using Shop.Core.Dtos.ProductParameterValue;
using Shop.Core.Dtos.ProductTranslation;
using Shop.Infrastructure.Entities;

namespace Shop.Core.Dtos.Product;

public class ProductRequestFormDto
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public List<PriceFormDto> Prices { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductParameterValueFormDto> ProductParameterValues { get; set; }

    public List<ProductTranslationFormDto> Translations { get; set; }

    public ProductEntity ToEntity() => new()
    {
        IsActive = IsActive,
        Name = Name,
        ProductBaseId = ProductBaseId,
        ProductParameterValues = ProductParameterValues.Where(x => x.Value != null && x.Value != string.Empty).Select(x => x.ToEntity()).ToList(),
        Prices = Prices.Select(x => x.ToEntity()).ToList(),
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };
}