namespace Shared.Domain.Bases;

public abstract class BaseTranslatableEntity<T> : BaseEntity where T : BaseTranslationEntity
{
    public ICollection<T> Translations { get; set; }

    protected void UpdateTranslations(IEnumerable<T> entities)
    {
        foreach (var translation in entities)
        {
            var result = Translations.FirstOrDefault(x => x.Id == translation.Id);
            if (result is null)
            {
                Translations.Add(translation);
                continue;
            }

            result.Update(translation);
        }

        foreach (var translation in Translations.ToList())
        {
            if (!entities.Any(x => x.Id == translation.Id))
                Translations.Remove(translation);
        }
    }
}