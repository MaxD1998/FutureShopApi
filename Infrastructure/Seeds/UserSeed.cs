using Domain.Entities;
using Shared.Constants;
using Crypt = BCrypt.Net.BCrypt;

namespace Infrastructure.Seeds;

public class UserSeed
{
    public static UserEntity Seed() => new()
    {
        Id = new Guid(EntityIdConst.UserId),
        CreateTime = DateTime.UtcNow,
        FirstName = "Super",
        LastName = "Admin",
        DateOfBirth = DateOnly.MinValue,
        Email = "superadmin@futureshop.pl",
        HashedPassword = Crypt.HashPassword("123456789")
    };
}