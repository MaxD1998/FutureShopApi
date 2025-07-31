using Shared.Domain.Bases;
using Shop.Domain.Aggregates.Products;

namespace Shop.Domain.Aggregates.Products.Entities;

public class ProductTranslationEntity : BaseTranslationEntity<ProductTranslationEntity>
{
    public ProductTranslationEntity(Guid id, string lang, string translation) : base(id, lang, translation)
    {
    }

    private ProductTranslationEntity() : base()
    {
    }

    public Guid ProductId { get; set; }

    #region Related Data

    public ProductAggregate Product { get; set; }

    #endregion Related Data
}