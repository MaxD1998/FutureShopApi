using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;
using Shop.Domain.Entities.Products;
using Shared.Domain.Extensions;

namespace Shop.Domain.Entities.ProductBases;

public class ProductParameterEntity : BaseEntity, IUpdate<ProductParameterEntity>, IEntityValidation
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; private set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; private set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; private set; } = [];

    public ICollection<ProductParameterTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(ProductParameterEntity entity)
    {
        Name = entity.Name;
        Translations.UpdateEntities(entity.Translations);
    }

    public void Validate()
    {
        ValidateName();

        Translations.ValidateEntities();
    }

    private void ValidateName()
    {
        var length = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    #endregion Methods
}