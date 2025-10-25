using Shared.Infrastructure.Constants;
using Shop.Infrastructure.Entities.Users;

namespace Shop.Infrastructure.Seeds;

public class UserSeed
{
    public static UserEntity Seed() => new()
    {
        Id = new Guid(EntityIdConst.UserId),
        ExternalId = new Guid(EntityIdConst.UserId),
        CreateTime = DateTime.MinValue,
        FirstName = "Super",
        LastName = "Admin",
    };
}