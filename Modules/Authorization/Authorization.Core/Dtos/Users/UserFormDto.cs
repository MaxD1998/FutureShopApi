using Authorization.Domain.Aggregates.Users;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Dtos.Users;

public class UserFormDto
{
    public DateTime DateOfBirth { get; set; } = new DateTime(1900, 1, 1);

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public User ToUser() => new(DateOnly.FromDateTime(DateOfBirth), Email, FirstName, LastName, Crypt.HashPassword(Password), PhoneNumber);
}