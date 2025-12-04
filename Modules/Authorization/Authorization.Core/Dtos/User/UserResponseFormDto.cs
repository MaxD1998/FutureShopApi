using Authorization.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.User;

public class UserResponseFormDto : UserUpdateRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<UserEntity, UserResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email,
        Type = entity.Type,
        UserPermissionGroups = entity.UserPermissionGroups.AsQueryable().Select(UserPermissionGroupFromDto.Map()).ToList()
    };
}