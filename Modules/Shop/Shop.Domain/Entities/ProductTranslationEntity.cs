using Shared.Domain.Bases;

namespace Shop.Domain.Entities;

public class ProductTranslationEntity : BaseTranslationEntity<ProductTranslationEntity>
{
    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data
}