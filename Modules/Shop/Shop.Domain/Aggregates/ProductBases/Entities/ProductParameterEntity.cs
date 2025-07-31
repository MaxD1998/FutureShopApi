using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.ProductBases;
using Shop.Domain.Aggregates.Products.Entities;

namespace Shop.Domain.Aggregates.ProductBases.Entities;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseAggregate ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductParameterTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        Translations.UpdateEntities(entity.Translations);
    }
}