using Product.Domain.Aggregates.Categories.Comparers;
using Product.Domain.Aggregates.ProductBases;
using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Product.Domain.Aggregates.Categories;

public class CategoryAggregate : BaseEntity, IUpdate<CategoryAggregate>
{
    private HashSet<CategoryAggregate> _subCategories = [];

    public CategoryAggregate(string name, Guid? parentCategoryId)
    {
        SetName(name);
        SetParentCategoryId(parentCategoryId);
    }

    private CategoryAggregate()
    {
    }

    public string Name { get; private set; }

    public Guid? ParentCategoryId { get; private set; }

    #region Related Data

    public IReadOnlyCollection<ProductBaseAggregate> ProductBases { get; private set; } = [];

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

    public void SetParentCategoryId(Guid? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
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
        _subCategories = CategoryAggregateComparer.CreateSet(entity.SubCategories);
    }

    #endregion Methods
}