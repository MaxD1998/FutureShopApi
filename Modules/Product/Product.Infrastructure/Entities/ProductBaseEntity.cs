using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Entities;

public class ProductBaseEntity : BaseEntity, IUpdate<ProductBaseEntity>, IEntityValidation
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    #region Related Data

    public CategoryEntity Category { get; private set; }

    public ICollection<ProductEntity> Products { get; private set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(ProductBaseEntity entity)
    {
        CategoryId = entity.CategoryId;
        Name = entity.Name;
    }

    public void Validate()
    {
        ValidateCategoryId();
        ValidateName();
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