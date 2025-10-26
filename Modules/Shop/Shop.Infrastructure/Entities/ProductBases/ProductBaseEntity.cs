using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Categories;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Entities.ProductBases;

public class ProductBaseEntity : BaseExternalEntity, IUpdate<ProductBaseEntity>, IUpdateEvent<ProductBaseEntity>, IEntityValidation
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryEntity Category { get; private set; }

    public ICollection<ProductParameterEntity> ProductParameters { get; set; } = [];

    public ICollection<ProductEntity> Products { get; private set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(ProductBaseEntity entity)
    {
        ProductParameters.UpdateEntities(entity.ProductParameters);
    }

    public void UpdateEvent(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
    }

    public void Validate()
    {
        ValidateCategoryId();
        ValidateName();

        ProductParameters.ValidateEntities();
    }

    private void ValidateCategoryId()
    {
        if (CategoryId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(CategoryId));
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