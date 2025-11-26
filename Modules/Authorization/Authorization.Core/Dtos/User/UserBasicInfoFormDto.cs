using Authorization.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.User;

public class UserBasicInfoFormDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public static Expression<Func<UserEntity, UserBasicInfoFormDto>> Map() => entity => new()
    {
        FirstName = entity.FirstName,
        LastName = entity.LastName,
    };

    public UserEntity ToEntity() => new()
    {
        FirstName = FirstName,
        LastName = LastName,
    };
}