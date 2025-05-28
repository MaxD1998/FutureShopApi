using Authorization.Domain.Entities;
using Shared.Domain.Enums;
using Shared.Infrastructure.Constants;

namespace Authorization.Inrfrastructure.Seeds;

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