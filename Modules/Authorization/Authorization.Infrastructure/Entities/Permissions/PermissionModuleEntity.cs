using Authorization.Infrastructure.Entities.PrermissionGroups;
using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Enums;

namespace Authorization.Infrastructure.Entities.Permissions;

public class PermissionModuleEntity : BaseEntity
{
    public ModuleType Type { get; set; }

    public Guid? UserId { get; set; }

    public Guid? UserPermissionGroupId { get; set; }

    #region Related Data

    public ICollection<AuthorizationPermissionEntity> AuthorizationPermissions { get; set; }

    public ICollection<ProductPermissionEntity> ProductPermissions { get; set; }

    public ICollection<ShopPermissionEntity> ShopPermissions { get; set; }

    public UserEntity User { get; set; }

    public PermissionGroupEntity UserPermissionGroup { get; set; }

    public ICollection<WarehousePermissionEntity> WarehousePermissions { get; set; }

    #endregion Related Data
}