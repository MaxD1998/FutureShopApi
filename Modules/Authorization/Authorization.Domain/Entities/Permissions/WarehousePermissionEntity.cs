using Authorization.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Domain.Entities.Permissions;

public class WarehousePermissionEntity : BasePermissionEntity<WarehousePermission>, IUpdate<WarehousePermissionEntity>
{
    public void Update(WarehousePermissionEntity entity)
    {
    }
}