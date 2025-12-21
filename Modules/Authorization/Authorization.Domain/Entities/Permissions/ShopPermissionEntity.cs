using Authorization.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Domain.Entities.Permissions;

public class ShopPermissionEntity : BasePermissionEntity<ShopPermission>, IUpdate<ShopPermissionEntity>
{
    public void Update(ShopPermissionEntity entity)
    {
    }
}