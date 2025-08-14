using Shared.Infrastructure.Interfaces;

namespace Shared.Infrastructure.Bases;

public abstract class BaseEntity : IEntity
{
    public DateTime CreateTime { get; set; }

    public Guid Id { get; set; }

    public DateTime? ModifyTime { get; set; }
}