using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities;

public class CategoryEntity : BaseExternalEntity, IUpdate<CategoryEntity>, IUpdateEvent<CategoryEntity>
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryEntity ParentCategory { get; set; }

    public ICollection<ProductBaseEntity> ProductBases { get; set; } = [];

    public ICollection<CategoryEntity> SubCategories { get; set; } = [];

    public ICollection<CategoryTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

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
}