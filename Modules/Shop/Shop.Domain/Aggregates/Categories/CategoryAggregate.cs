using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;
using Shop.Domain.Aggregates.Categories.Entities;
using Shop.Domain.Aggregates.ProductBases;

namespace Shop.Domain.Aggregates.Categories;

public class CategoryAggregate : BaseExternalEntity, IUpdate<CategoryAggregate>, IUpdateEvent<CategoryAggregate>
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryAggregate ParentCategory { get; set; }

    public ICollection<ProductBaseAggregate> ProductBases { get; set; } = [];

    public ICollection<CategoryAggregate> SubCategories { get; set; } = [];

    public ICollection<CategoryTranslationEntity> Translations { get; set; } = [];

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

    public void Update(CategoryAggregate entity)
    {
        IsActive = entity.IsActive;
        Translations.UpdateEntities(entity.Translations);
    }

    public void UpdateEvent(CategoryAggregate entity)
    {
        Name = entity.Name;
        ParentCategory = entity.ParentCategory;
        SubCategories = entity.SubCategories;
    }
}