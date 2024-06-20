using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductParameterTranslationEntity : BaseTranslationEntity
{
    public Guid ProductParameterId { get; set; }

    #region Related Data

    public ProductParameterEntity ProductParameter { get; set; }

    #endregion Related Data
}