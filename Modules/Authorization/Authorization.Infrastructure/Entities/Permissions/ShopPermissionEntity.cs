using Authorization.Infrastructure.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Infrastructure.Entities.Permissions;

public class ShopPermissionEntity : BasePermissionEntity<ShopPermission>, IUpdate<ShopPermissionEntity>
{
    public void Update(ShopPermissionEntity entity)
    {
    }
}