using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class CategoryEntity : BaseEntity
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
        Name = entity.Name;
        ParentCategoryId = entity.ParentCategoryId;
        SubCategories = entity.SubCategories;
        UpdateTranslations(entity.Translations);
    }

    private void UpdateTranslations(ICollection<CategoryTranslationEntity> entities)
    {
        foreach (var translation in entities)
        {
            var result = Translations.FirstOrDefault(x => x.Lang == translation.Lang);
            if (result is null)
            {
                Translations.Add(translation);
                continue;
            }

            result.Update(translation);
        }

        foreach (var translation in Translations.ToList())
        {
            if (!entities.Any(x => x.Lang == translation.Lang))
                Translations.Remove(translation);
        }
    }
}