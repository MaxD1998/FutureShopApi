using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        Translations.UpdateEntities(entity.Translations);
    }
}