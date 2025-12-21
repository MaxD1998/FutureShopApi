using Authorization.Domain.Entities.Users;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Dtos.Register;

public class RegisterFormDto
{
    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public UserEntity ToEntity() => new()
    {
        Email = Email,
        FirstName = FirstName,
        LastName = LastName,
        HashedPassword = Crypt.HashPassword(Password),
    };
}