using Authorization.Core.Bases;
using Authorization.Infrastructure.Entities.Permissions;
using Shared.Shared.Enums;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup.Permissions;

public class ProductPermissionFormDto : BasePermissionFormDto<ProductPermission>
{
    public Guid? Id { get; set; }

    public static Expression<Func<ProductPermissionEntity, ProductPermissionFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Permission = entity.Permission,
    };

    public ProductPermissionEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Permission = Permission,
    };
}