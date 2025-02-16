using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Entities;

public class CategoryEntity : BaseExternalEntity, IUpdate<CategoryEntity>, IUpdateEvent<CategoryEntity>
{
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
        Translations.UpdateEntities(entity.Translations);
    }

    public void UpdateEvent(CategoryEntity entity)
    {
        Name = entity.Name;
        ParentCategory = entity.ParentCategory;
        SubCategories = entity.SubCategories;
    }
}