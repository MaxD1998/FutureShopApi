using Shared.Domain.Bases;
using Shop.Domain.Aggregates.Categories;

namespace Shop.Domain.Aggregates.Categories.Entities;

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

    public CategoryAggregate Category { get; set; }

    #endregion Related Data
}