using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductParameterTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        Translations.UpdateEntities(entity.Translations);
    }
}