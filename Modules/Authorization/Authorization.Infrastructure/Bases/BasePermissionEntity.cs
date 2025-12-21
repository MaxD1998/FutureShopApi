using Authorization.Infrastructure.Entities.PrermissionGroups;
using Shared.Domain.Bases;

namespace Authorization.Infrastructure.Bases;

public class BasePermissionEntity<TPermissionEnum> : BaseEntity where TPermissionEnum : Enum
{
    public TPermissionEnum Permission { get; set; }

    public Guid PermissionGroupId { get; set; }

    #region Related Data

    public PermissionGroupEntity PermissionGroup { get; set; }

    #endregion Related Data
}