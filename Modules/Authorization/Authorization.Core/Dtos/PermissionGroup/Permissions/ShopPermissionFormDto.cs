using Authorization.Core.Bases;
using Authorization.Infrastructure.Entities.Permissions;
using Shared.Shared.Enums;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup.Permissions;

public class ShopPermissionFormDto : BasePermissionFormDto<ShopPermission>
{
    public Guid? Id { get; set; }

    public static Expression<Func<ShopPermissionEntity, ShopPermissionFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Permission = entity.Permission,
    };

    public ShopPermissionEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Permission = Permission,
    };
}