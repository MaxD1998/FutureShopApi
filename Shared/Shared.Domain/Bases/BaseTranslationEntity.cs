using Shared.Domain.Interfaces;

namespace Shared.Domain.Bases;

public abstract class BaseTranslationEntity<TEntity> : BaseEntity, IUpdate<TEntity> where TEntity : BaseTranslationEntity<TEntity>
{
    protected BaseTranslationEntity()
    {
    }

    protected BaseTranslationEntity(Guid id, string lang, string translation)
    {
        Id = id;
        SetLang(lang);
        SetTranslation(translation);
    }

    public string Lang { get; private set; }

    public string Translation { get; private set; }

    #region Setters

    public void SetLang(string lang)
    {
        ValidateRequiredLangStringProperty(nameof(Lang), lang);

        Lang = lang;
    }

    public void SetTranslation(string translation)
    {
        ValidateRequiredProperty(nameof(Translation), translation);

        Translation = translation;
    }

    #endregion Setters

    #region Methods

    public void Update(TEntity entity)
    {
        Translation = entity.Translation;
    }

    #endregion Methods
}