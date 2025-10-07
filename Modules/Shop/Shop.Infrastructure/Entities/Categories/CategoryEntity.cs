﻿using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.ProductBases;

namespace Shop.Infrastructure.Entities.Categories;

public class CategoryEntity : BaseExternalEntity, IUpdate<CategoryEntity>, IUpdateEvent<CategoryEntity>
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryEntity ParentCategory { get; set; }

    public ICollection<ProductBaseEntity> ProductBases { get; private set; } = [];

    public ICollection<CategoryEntity> SubCategories { get; set; } = [];

    public ICollection<CategoryTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(CategoryEntity entity)
    {
        IsActive = entity.IsActive;
        Translations.UpdateEntities(entity.Translations);
    }

    public void UpdateEvent(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategory = entity.ParentCategory;
        SubCategories = entity.SubCategories;
    }

    public void Validate()
    {
        ValidateExternalId();
        ValidateName();
    }

    private void ValidateExternalId()
    {
        if (ExternalId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ExternalId));
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