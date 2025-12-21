namespace Shared.Domain.Bases;

public class BaseExternalEntity : BaseEntity
{
    public Guid ExternalId { get; set; }
}