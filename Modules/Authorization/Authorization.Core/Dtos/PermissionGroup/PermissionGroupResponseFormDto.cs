using Authorization.Core.Dtos.PermissionGroup.Permissions;
using Authorization.Infrastructure.Entities.PrermissionGroups;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.PermissionGroup;

public class PermissionGroupResponseFormDto : PermissionGroupRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<PermissionGroupEntity, PermissionGroupResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        AuthorizationPermissions = entity.AuthorizationPermissions.AsQueryable().Select(AuthorizationPermissionFormDto.Map()).ToList(),
        ProductPermissions = entity.ProductPermissions.AsQueryable().Select(ProductPermissionFormDto.Map()).ToList(),
        ShopPermissions = entity.ShopPermissions.AsQueryable().Select(ShopPermissionFormDto.Map()).ToList(),
        WarehousePermissions = entity.WarehousePermissions.AsQueryable().Select(WarehousePermissionFormDto.Map()).ToList(),
    };
}