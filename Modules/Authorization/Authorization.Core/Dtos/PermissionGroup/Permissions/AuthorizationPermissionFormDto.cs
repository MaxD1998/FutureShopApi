using Authorization.Core.Bases;
using Authorization.Domain.Entities.Permissions;
using Shared.Shared.Enums;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup.Permissions;

public class AuthorizationPermissionFormDto : BasePermissionFormDto<AuthorizationPermission>
{
    public Guid? Id { get; set; }

    public static Expression<Func<AuthorizationPermissionEntity, AuthorizationPermissionFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Permission = entity.Permission,
    };

    public AuthorizationPermissionEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Permission = Permission,
    };
}