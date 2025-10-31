using Authorization.Infrastructure.Entities.Permissions;
using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Entities.PrermissionGroups;

public class PermissionGroupEntity : BaseEntity
{
    public string Name { get; set; }

    #region Related Data

    public ICollection<AuthorizationPermissionEntity> AuthorizationPermissions { get; set; } = [];

    public ICollection<ProductPermissionEntity> ProductPermissions { get; set; } = [];

    public ICollection<ShopPermissionEntity> ShopPermissions { get; set; } = [];

    public ICollection<UserPermissionGroupEntity> UserPermissionGroups { get; set; } = [];

    public ICollection<WarehousePermissionEntity> WarehousePermissions { get; set; } = [];

    #endregion Related Data
}