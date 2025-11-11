using Authorization.Infrastructure.Entities.Users;
using Shared.Infrastructure.Enums;

namespace Authorization.Core.Dtos.User;

public class UserUpdateRequestFormDto
{
    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public UserType Type { get; set; }

    public List<UserPermissionGroupFromDto> UserPermissionGroups { get; set; }

    public virtual UserEntity ToEntity() => new()
    {
        DateOfBirth = DateOnly.FromDateTime(DateOfBirth),
        Email = Email,
        FirstName = FirstName,
        LastName = LastName,
        Type = Type,
        UserPermissionGroups = UserPermissionGroups.Select(x => x.ToEntity()).ToList()
    };
}