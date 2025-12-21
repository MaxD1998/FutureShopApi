using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Product.Domain.Entities;

public class CategoryEntity : BaseEntity, IUpdate<CategoryEntity>, IEntityValidation
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryEntity ParentCategory { get; set; }

    public ICollection<ProductBaseEntity> ProductBases { get; private set; } = [];

    public ICollection<CategoryEntity> SubCategories { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        SubCategories = entity.SubCategories;
    }

    public void Validate()
    {
        ValidateName();
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