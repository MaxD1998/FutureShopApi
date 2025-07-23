using Product.Domain.Aggregates.ProductBases;
using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Product.Domain.Aggregates.Categories;

public class CategoryAggregate : BaseEntity, IUpdate<CategoryAggregate>
{
    private HashSet<CategoryAggregate> _subCategories = [];

    public CategoryAggregate(string name)
    {
        SetName(name);
    }

    private CategoryAggregate()
    {
    }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public ICollection<ProductBaseAggregate> ProductBases { get; private set; } = [];

    public IReadOnlyCollection<CategoryAggregate> SubCategories => _subCategories;

    #endregion Related Data

    #region Setters

    public void SetName(string name)
    {
        var propertyName = nameof(name);
        var maxLength = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(name))
            throw new PropertyWasEmptyException(propertyName);

        if (name.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        Name = name;
    }

    #endregion Setters

    #region Methods

    public void AddSubCategory(CategoryAggregate subCategory)
    {
        if (subCategory == null)
            throw new ArgumentNullException(nameof(subCategory));

        subCategory.ParentCategoryId = ParentCategoryId;

        _subCategories.Add(subCategory);
    }

    public void Update(CategoryAggregate entity)
    {
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        _subCategories = entity.SubCategories.ToHashSet();
    }

    #endregion Methods
}