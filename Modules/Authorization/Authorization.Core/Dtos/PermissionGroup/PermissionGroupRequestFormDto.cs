using Authorization.Core.Dtos.PermissionGroup.Permissions;
using Authorization.Domain.Entities.PrermissionGroups;

namespace Authorization.Core.Dtos.PermissionGroup;

public class PermissionGroupRequestFormDto
{
    public List<AuthorizationPermissionFormDto> AuthorizationPermissions { get; set; } = [];

    public string Name { get; set; }

    public List<ProductPermissionFormDto> ProductPermissions { get; set; } = [];

    public List<ShopPermissionFormDto> ShopPermissions { get; set; } = [];

    public List<WarehousePermissionFormDto> WarehousePermissions { get; set; } = [];

    public PermissionGroupEntity ToEntity() => new()
    {
        Name = Name,
        AuthorizationPermissions = AuthorizationPermissions.Select(x => x.ToEntity()).ToList(),
        ProductPermissions = ProductPermissions.Select(x => x.ToEntity()).ToList(),
        ShopPermissions = ShopPermissions.Select(x => x.ToEntity()).ToList(),
        WarehousePermissions = WarehousePermissions.Select(x => x.ToEntity()).ToList(),
    };
}