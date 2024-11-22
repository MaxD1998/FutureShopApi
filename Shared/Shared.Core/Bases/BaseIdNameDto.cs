using Shared.Core.Interfaces;

namespace Shared.Core.Bases;

public abstract class BaseIdNameDto : IDto
{
    protected BaseIdNameDto()
    {
    }

    protected BaseIdNameDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}