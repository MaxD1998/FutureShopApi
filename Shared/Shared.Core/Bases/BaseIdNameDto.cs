using Shared.Core.Interfaces;

namespace Shared.Core.Bases;

public abstract class BaseIdNameDto : IDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}