using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Persistence.Entities.ProductBases;

namespace Shop.Infrastructure.Persistence.Entities.Products;

public class ProductParameterValueEntity : BaseEntity, IUpdate<ProductParameterValueEntity>, IEntityValidation
{
    public Guid ProductId { get; private set; }

    public Guid ProductParameterId { get; set; }

    public string Value { get; set; }

    #region Related Data

    public ProductEntity Product { get; private set; }

    public ProductParameterEntity ProductParameter { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(ProductParameterValueEntity entity)
    {
        Value = entity.Value;
    }

    public void Validate()
    {
        ValidateValue();
    }

    private void ValidateValue()
    {
        var length = StringLengthConst.MiddleString;

        if (string.IsNullOrWhiteSpace(Value))
            throw new PropertyWasEmptyException(nameof(Value));

        if (Value.Length > length)
            throw new PropertyWasTooLongException(nameof(Value), length);
    }

    #endregion Methods
}