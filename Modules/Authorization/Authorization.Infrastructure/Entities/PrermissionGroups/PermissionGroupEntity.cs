using Authorization.Infrastructure.Entities.Permissions;
using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Bases;

namespace Authorization.Infrastructure.Entities.PrermissionGroups;

public class PermissionGroupEntity : BaseEntity
{
    public string Name { get; set; }

    #region Related Data

    public ICollection<PermissionModuleEntity> UserPermissionModules { get; set; } = [];

    public ICollection<UserEntity> Users { get; set; } = [];

    #endregion Related Data
}