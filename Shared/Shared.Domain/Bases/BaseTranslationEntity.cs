namespace Shared.Domain.Bases;

public class BaseTranslationEntity : BaseEntity
{
    public string Lang { get; private set; }

    public string Translation { get; set; }
}