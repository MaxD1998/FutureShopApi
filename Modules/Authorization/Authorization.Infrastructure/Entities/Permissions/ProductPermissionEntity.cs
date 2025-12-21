using Authorization.Infrastructure.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Infrastructure.Entities.Permissions;

public class ProductPermissionEntity : BasePermissionEntity<ProductPermission>, IUpdate<ProductPermissionEntity>
{
    public void Update(ProductPermissionEntity entity)
    {
    }
}