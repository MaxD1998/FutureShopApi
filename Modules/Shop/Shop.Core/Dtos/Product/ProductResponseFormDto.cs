using Shop.Core.Dtos.Price;
using Shop.Core.Dtos.ProductParameterValue;
using Shop.Core.Dtos.ProductTranslation;
using Shop.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductResponseFormDto : ProductRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<ProductEntity, ProductResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        IsActive = entity.IsActive,
        Name = entity.Name,
        Prices = entity.Prices.AsQueryable().Select(PriceFormDto.Map()).ToList(),
        ProductBaseId = entity.ProductBaseId,
        ProductParameterValues = entity.ProductBase.ProductParameters.AsQueryable().Select(productParameter => new ProductParameterValueFormDto
        {
            Id = productParameter.ProductParameterValues.AsQueryable().Where(value => value.ProductId == entity.Id).Select(x => (Guid?)x.Id).FirstOrDefault(),
            ProductParameterId = productParameter.Id,
            ProductParameterName = productParameter.Name,
            Value = productParameter.ProductParameterValues.AsQueryable().Where(value => value.ProductId == entity.Id).Select(x => x.Value).FirstOrDefault(),
        }).ToList(),
        Translations = entity.Translations.AsQueryable().Select(ProductTranslationFormDto.Map()).ToList(),
    };
}