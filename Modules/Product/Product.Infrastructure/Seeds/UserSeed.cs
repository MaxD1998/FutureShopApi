using Product.Domain.Entities;
using Shared.Infrastructure.Constants;

namespace Product.Infrastructure.Seeds;

public class UserSeed
{
    public static UserEntity Seed() => new()
    {
        Id = new Guid(EntityIdConst.UserId),
        CreateTime = DateTime.UtcNow,
        FirstName = "Super",
        LastName = "Admin",
    };
}