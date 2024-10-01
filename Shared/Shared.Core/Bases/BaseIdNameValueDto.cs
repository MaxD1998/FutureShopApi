namespace Shared.Core.Bases;

public abstract class BaseIdNameValueDto : BaseIdNameDto
{
    public BaseIdNameValueDto(Guid id, string name, string value) : base(id, name)
    {
        Value = value;
    }

    public string Value { get; }
}