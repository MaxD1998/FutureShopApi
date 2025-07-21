using Shared.Domain.Interfaces;

namespace Shared.Domain.Bases;

public abstract class BaseEntity : IEntity
{
    public DateTime CreateTime { get; protected set; }

    public Guid Id { get; set; }

    public DateTime? ModifyTime { get; protected set; }

    public void MarkCreated()
    {
        if (CreateTime == default)
            CreateTime = DateTime.UtcNow;
    }

    public void MarkModified()
    {
        ModifyTime = DateTime.UtcNow;
    }
}