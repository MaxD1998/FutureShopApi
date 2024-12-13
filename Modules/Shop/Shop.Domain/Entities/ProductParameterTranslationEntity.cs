using Shared.Domain.Bases;

namespace Shop.Domain.Entities;

public class ProductParameterTranslationEntity : BaseTranslationEntity<ProductParameterTranslationEntity>
{
    public Guid ProductParameterId { get; set; }

    #region Related Data

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data
}