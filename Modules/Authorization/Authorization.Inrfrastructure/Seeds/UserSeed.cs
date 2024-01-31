using Authorization.Domain.Entities;
using Shared.Infrastructure.Constants;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Inrfrastructure.Seeds;

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