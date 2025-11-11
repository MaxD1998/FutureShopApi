namespace Authorization.Core.Bases;

public class BasePermissionFormDto<TPermissionEnum> where TPermissionEnum : Enum
{
    public TPermissionEnum Permission { get; set; }
}