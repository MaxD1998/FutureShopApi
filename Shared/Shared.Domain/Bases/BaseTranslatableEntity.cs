namespace Shared.Domain.Bases;

public abstract class BaseTranslatableEntity<T> : BaseEntity where T : BaseTranslationEntity
{
    public ICollection<T> Translations { get; set; }

    protected void UpdateTranslations(IEnumerable<T> entities)
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