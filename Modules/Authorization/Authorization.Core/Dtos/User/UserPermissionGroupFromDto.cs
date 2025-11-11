using Authorization.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.User;

public class UserPermissionGroupFromDto
{
    public Guid? Id { get; set; }

    public Guid PermissionGroupId { get; set; }

    public string PermissionGroupName { get; set; }

    public static Expression<Func<UserPermissionGroupEntity, UserPermissionGroupFromDto>> Map() => entity => new()
    {
        Id = entity.Id,
        PermissionGroupId = entity.PermissionGroupId,
        PermissionGroupName = entity.PermissionGroup.Name
    };

    public UserPermissionGroupEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        PermissionGroupId = PermissionGroupId
    };
}