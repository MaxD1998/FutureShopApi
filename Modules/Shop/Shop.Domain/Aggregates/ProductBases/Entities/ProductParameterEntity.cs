using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shop.Domain.Aggregates.Products.Entities;

namespace Shop.Domain.Aggregates.ProductBases.Entities;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>
{
    private HashSet<ProductParameterTranslationEntity> _translations;

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseAggregate ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public IReadOnlyCollection<ProductParameterTranslationEntity> Translations => _translations;

    #endregion Related Data

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        _translations.UpdateEntities(entity.Translations);
    }
}