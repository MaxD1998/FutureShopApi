﻿using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Product.Domain.Entities;

public class CategoryEntity : BaseEntity, IUpdate<CategoryEntity>
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryEntity ParentCategory { get; set; }

    public ICollection<ProductBaseEntity> ProductBases { get; set; } = [];

    public ICollection<CategoryEntity> SubCategories { get; set; } = [];

    #endregion Related Data

    public void Update(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        SubCategories = entity.SubCategories;
    }
}