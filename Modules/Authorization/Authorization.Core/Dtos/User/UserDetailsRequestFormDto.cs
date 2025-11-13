using Authorization.Infrastructure.Entities.Users;

namespace Authorization.Core.Dtos.User;

public class UserDetailsRequestFormDto
{
    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public UserEntity ToEntity() => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
    };
}