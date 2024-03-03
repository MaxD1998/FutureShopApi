using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    #region Related Data

    public CategoryEntity ParentCategory { get; set; }

    public ICollection<ProductBaseEntity> ProductBases { get; set; }

    public ICollection<CategoryEntity> SubCategories { get; set; }

    public ICollection<CategoryTranslationEntity> Translations { get; set; }

    #endregion Related Data
}