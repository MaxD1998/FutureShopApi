using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;

namespace Product.Infrastructure.Entities;

public class ProductEntity : BaseEntity, IUpdate<ProductEntity>, IEntityValidation
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; private set; }

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(ProductEntity entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEntities(entity.ProductPhotos);
    }

    public void Validate()
    {
        ValidateName();
        ValidateProductBaseId();

        ProductPhotos.ValidateEntities();
    }

    private void ValidateName()
    {
        var length = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    private void ValidateProductBaseId()
    {
        if (ProductBaseId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductBaseId));
    }

    #endregion Methods
}