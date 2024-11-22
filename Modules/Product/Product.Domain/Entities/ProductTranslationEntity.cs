using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductTranslationEntity : BaseTranslationEntity<ProductTranslationEntity>
{
    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data
}