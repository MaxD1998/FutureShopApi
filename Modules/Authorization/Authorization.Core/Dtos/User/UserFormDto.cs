using Authorization.Domain.Entities;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Dtos.User;

public class UserFormDto
{
    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public UserEntity ToEntity() => new()
    {
        DateOfBirth = DateOnly.FromDateTime(DateOfBirth),
        Email = Email,
        FirstName = FirstName,
        LastName = LastName,
        HashedPassword = Crypt.HashPassword(Password),
        PhoneNumber = PhoneNumber
    };
}