using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductParameterEntity : BaseTranslatableEntity<ProductParameterTranslationEntity>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        UpdateTranslations(entity.Translations);
    }
}