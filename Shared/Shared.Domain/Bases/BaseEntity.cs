namespace Shared.Domain.Bases;

public abstract class BaseEntity
{
    public DateTime CreateTime { get; set; }

    public Guid Id { get; set; }

    public DateTime? ModifyTime { get; set; }
}