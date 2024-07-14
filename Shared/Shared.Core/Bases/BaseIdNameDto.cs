using Shared.Core.Interfaces;

namespace Shared.Core.Bases;

public abstract class BaseIdNameDto : IDto
{
    public BaseIdNameDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; }
}