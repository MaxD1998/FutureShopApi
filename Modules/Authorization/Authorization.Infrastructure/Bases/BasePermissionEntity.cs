using Authorization.Infrastructure.Entities.Permissions;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Bases;

public class BasePermissionEntity<TPermissionEnum> : BaseEntity where TPermissionEnum : Enum
{
    public TPermissionEnum Permission { get; set; }

    public Guid UserPermissionModuleId { get; set; }

    #region Related Data

    public PermissionModuleEntity UserPermissionModule { get; set; }

    #endregion Related Data
}