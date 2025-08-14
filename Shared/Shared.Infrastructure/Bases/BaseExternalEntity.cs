namespace Shared.Infrastructure.Bases;

public class BaseExternalEntity : BaseEntity
{
    public Guid ExternalId { get; set; }
}