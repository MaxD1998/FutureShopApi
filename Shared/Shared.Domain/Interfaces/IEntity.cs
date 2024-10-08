namespace Shared.Domain.Interfaces;

public interface IEntity
{
    DateTime CreateTime { get; }

    Guid Id { get; }

    DateTime? ModifyTime { get; }
}