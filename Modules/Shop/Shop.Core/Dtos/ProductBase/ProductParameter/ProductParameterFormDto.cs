using Shop.Core.Dtos.Product.ProductParameterTranslation;
using Shop.Infrastructure.Entities.ProductBases;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductBase.ProductParameter;

public class ProductParameterFormDto
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public List<ProgramParameterTranslationFormDto> Translations { get; set; }

    public static Expression<Func<ProductParameterEntity, ProductParameterFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Translations = entity.Translations.AsQueryable().Select(ProgramParameterTranslationFormDto.Map()).ToList(),
    };

    public ProductParameterEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Name = Name,
        Translations = Translations.Select(x => x.ToEntity()).ToList()
    };
}