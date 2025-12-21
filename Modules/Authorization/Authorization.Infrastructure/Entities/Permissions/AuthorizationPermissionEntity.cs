using Authorization.Infrastructure.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Infrastructure.Entities.Permissions;

public class AuthorizationPermissionEntity : BasePermissionEntity<AuthorizationPermission>, IUpdate<AuthorizationPermissionEntity>
{
    public void Update(AuthorizationPermissionEntity entity)
    { }
}