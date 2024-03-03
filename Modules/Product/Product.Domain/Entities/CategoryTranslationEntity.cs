using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class CategoryTranslationEntity : BaseTranslationEntity
{
    public Guid CategoryId { get; set; }

    #region Related Data

    public CategoryEntity Category { get; set; }

    #endregion Related Data
}