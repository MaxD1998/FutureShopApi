using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductTranslationEntity : BaseTranslationEntity
{
    public Guid ProductId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    #endregion Related Data
}