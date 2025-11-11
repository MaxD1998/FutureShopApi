using Authorization.Infrastructure.Entities.Users;
using System.Linq.Expressions;

namespace Authorization.Core.Dtos.User;

public class UserDetailsResponseFormDto : UserDetailsRequestFormDto
{
    public Guid Id { get; set; }

    public static Expression<Func<UserEntity, UserDetailsResponseFormDto>> Map() => entity => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        DateOfBirth = entity.DateOfBirth,
        Email = entity.Email,
        PhoneNumber = entity.PhoneNumber,
    };
}