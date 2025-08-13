using Shared.Infrastructure.Bases;

namespace Shop.Infrastructure.Entities;

public class ProductParameterTranslationEntity : BaseTranslationEntity<ProductParameterTranslationEntity>
{
    public Guid ProductParameterId { get; set; }

    #region Related Data

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data
}