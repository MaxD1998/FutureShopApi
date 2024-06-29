namespace Shared.Core.Bases;

public abstract class BaseIdNameDto
{
    protected BaseIdNameDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; }
}