using Authorization.Infrastructure.Entities.Users;
using Crypt = BCrypt.Net.BCrypt;

namespace Authorization.Core.Dtos.User;

public class UserCreateRequestFormDto : UserUpdateRequestFormDto
{
    public string Password { get; set; }

    public override UserEntity ToEntity()
    {
        var entity = base.ToEntity();
        entity.HashedPassword = Crypt.HashPassword(Password);

        return entity;
    }
}