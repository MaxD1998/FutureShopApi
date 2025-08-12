using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;
using Shop.Domain.Aggregates.Categories.Comparers;
using Shop.Domain.Aggregates.Categories.Entities;
using Shop.Domain.Aggregates.ProductBases;

namespace Shop.Domain.Aggregates.Categories;

public class CategoryAggregate : BaseExternalEntity, IUpdate<CategoryAggregate>, IUpdateEvent<CategoryAggregate>
{
    private HashSet<CategoryTranslationEntity> _translations = CategoryTranslateEntityComparer.CreateSet();

    public CategoryAggregate(Guid externalId, string name, Guid? parentCategoryId, List<CategoryAggregate> subCategories)
    {
        ExternalId = externalId;
        SetName(name);
        SetParentCategoryId(parentCategoryId);
        SubCategories = subCategories ?? [];
    }

    private CategoryAggregate()
    {
    }

    public bool IsActive { get; private set; } = false;

    public string Name { get; private set; }

    public Guid? ParentCategoryId { get; private set; }

    #region Related Data

    public CategoryAggregate ParentCategory { get; private set; }

    public IReadOnlyCollection<ProductBaseAggregate> ProductBases { get; private set; } = [];

    public IReadOnlyCollection<CategoryAggregate> SubCategories { get; private set; } = [];

    public IReadOnlyCollection<CategoryTranslationEntity> Translations => _translations;

    #endregion Related Data

    #region Setters

    private void SetName(string name)
    {
        var propertyName = nameof(name);
        var maxLength = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(name))
            throw new PropertyWasEmptyException(propertyName);

        if (name.Length > maxLength)
            throw new PropertyWasTooLongException(propertyName, maxLength);

        Name = name;
    }

    private void SetParentCategoryId(Guid? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
    }

    #endregion Setters

    #region Methods

    public void Update(CategoryAggregate entity)
    {
        IsActive = entity.IsActive;
        _translations.UpdateEntities(entity.Translations);
    }

    public void UpdateEvent(CategoryAggregate entity)
    {
        Name = entity.Name;
        ParentCategory = entity.ParentCategory;
        SubCategories = entity.SubCategories;
    }

    #endregion Methods
}