using Shared.Domain.Bases;
using Shop.Domain.Aggregates.ProductBases.Entities;

namespace Shop.Domain.Aggregates.Products.Entities;

public class ProductParameterTranslationEntity : BaseTranslationEntity<ProductParameterTranslationEntity>
{
    public ProductParameterTranslationEntity(Guid id, string lang, string translation) : base(id, lang, translation)
    {
    }

    private ProductParameterTranslationEntity() : base()
    {
    }

    public Guid ProductParameterId { get; set; }

    #region Related Data

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data
}