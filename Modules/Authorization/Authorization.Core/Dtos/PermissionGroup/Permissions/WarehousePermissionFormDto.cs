using Authorization.Core.Bases;
using Authorization.Infrastructure.Entities.Permissions;
using Shared.Shared.Enums;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup.Permissions;

public class WarehousePermissionFormDto : BasePermissionFormDto<WarehousePermission>
{
    public Guid? Id { get; set; }

    public static Expression<Func<WarehousePermissionEntity, WarehousePermissionFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Permission = entity.Permission,
    };

    public WarehousePermissionEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Permission = Permission,
    };
}