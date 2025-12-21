using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Shop.Domain.Entities.Categories;

public class CategoryTranslationEntity : BaseTranslationEntity<CategoryTranslationEntity>, IEntityValidation
{
    public Guid CategoryId { get; private set; }

    #region Related Data

    public CategoryEntity Category { get; private set; }

    #endregion Related Data

    #region Methods

    public void Validate()
    {
        ValidateLang();
        ValidateTranslation();
    }

    private void ValidateLang()
    {
        var length = StringLengthConst.LangString;

        if (string.IsNullOrWhiteSpace(Lang))
            throw new PropertyWasEmptyException(nameof(Lang));

        if (Lang.Length > length)
            throw new PropertyWasTooLongException(nameof(Lang), length);
    }

    private void ValidateTranslation()
    {
        if (string.IsNullOrWhiteSpace(Translation))
            throw new PropertyWasEmptyException(nameof(Translation));
    }

    #endregion Methods
}