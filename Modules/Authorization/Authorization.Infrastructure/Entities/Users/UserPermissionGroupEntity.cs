using Authorization.Infrastructure.Entities.PrermissionGroups;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Authorization.Infrastructure.Entities.Users;

public class UserPermissionGroupEntity : BaseEntity, IUpdate<UserPermissionGroupEntity>
{
    public Guid PermissionGroupId { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public PermissionGroupEntity PermissionGroup { get; set; }

    public UserEntity User { get; set; }

    #endregion Related Data

    public void Update(UserPermissionGroupEntity entity)
    {
    }
}