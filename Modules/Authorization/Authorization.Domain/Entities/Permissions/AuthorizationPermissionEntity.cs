using Authorization.Domain.Bases;
using Shared.Domain.Interfaces;
using Shared.Shared.Enums;

namespace Authorization.Domain.Entities.Permissions;

public class AuthorizationPermissionEntity : BasePermissionEntity<AuthorizationPermission>, IUpdate<AuthorizationPermissionEntity>
{
    public void Update(AuthorizationPermissionEntity entity)
    { }
}