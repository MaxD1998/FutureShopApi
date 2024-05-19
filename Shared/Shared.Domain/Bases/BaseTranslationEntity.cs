namespace Shared.Domain.Bases;

public class BaseTranslationEntity : BaseEntity
{
    public string Lang { get; set; }

    public string Translation { get; set; }

    public void Update(BaseTranslationEntity entity)
    {
        Translation = entity.Translation;
    }
}