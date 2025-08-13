using Authorization.Infrastructure.Entities;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Enums;

namespace Authorization.Infrastructure.Seeds;

public class UserSeed
{
    public static UserEntity Seed() => new()
    {
        Id = new Guid(EntityIdConst.UserId),
        CreateTime = DateTime.MinValue,
        FirstName = "Super",
        LastName = "Admin",
        DateOfBirth = DateOnly.MinValue,
        Email = "superadmin@futureshop.pl",
        HashedPassword = "$2a$11$v1B9qwcIeH.PJLuFjnmK7O1Nu3TSUsc6oZ49.5DXOJhkIDcfzPD..", // Crypt.HashPassword("123456789"),
        Type = UserType.SuperAdmin
    };
}