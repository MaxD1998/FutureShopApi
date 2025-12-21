using Authorization.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Domain.Entities.Permissions;

public class ProductPermissionEntity : BasePermissionEntity<ProductPermission>, IUpdate<ProductPermissionEntity>
{
    public void Update(ProductPermissionEntity entity)
    {
    }
}