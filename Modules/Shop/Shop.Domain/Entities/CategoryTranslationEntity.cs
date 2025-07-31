using Shared.Domain.Bases;

namespace Shop.Domain.Entities;

public class CategoryTranslationEntity : BaseTranslationEntity<CategoryTranslationEntity>
{
    public CategoryTranslationEntity(Guid id, string lang, string translation) : base(id, lang, translation)
    {
    }

    private CategoryTranslationEntity() : base()
    {
    }

    public Guid CategoryId { get; private set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    #endregion Related Data
}