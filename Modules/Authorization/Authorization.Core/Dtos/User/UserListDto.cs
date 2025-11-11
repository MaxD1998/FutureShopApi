using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Enums;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.User;

public class UserListDto
{
    public string FirstName { get; set; }

    public Guid Id { get; set; }

    public string LastName { get; set; }

    public UserType Type { get; set; }

    public int UserPermissionGroupCount { get; set; }

    public static Expression<Func<UserEntity, UserListDto>> Map() => entity => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Type = entity.Type,
        UserPermissionGroupCount = entity.UserPermissionGroups.AsQueryable().Count()
    };
}