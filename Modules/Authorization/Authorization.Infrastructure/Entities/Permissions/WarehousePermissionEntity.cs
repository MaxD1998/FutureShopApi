using Authorization.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Infrastructure.Entities.Permissions;

public class WarehousePermissionEntity : BasePermissionEntity<WarehousePermission>, IUpdate<WarehousePermissionEntity>
{
    public void Update(WarehousePermissionEntity entity)
    {
    }
}