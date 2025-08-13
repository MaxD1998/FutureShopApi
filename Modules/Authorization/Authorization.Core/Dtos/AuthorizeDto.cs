using Authorization.Core.Dtos.UserModule;
using Authorization.Infrastructure.Entities;
using Shared.Infrastructure.Enums;

namespace Authorization.Core.Dtos;

public class AuthorizeDto
{
    public AuthorizeDto(UserEntity entity, string token)
    {
        Id = entity.Id;
        Username = $"{entity.FirstName} {entity.LastName}";
        Token = token;
        Modules = entity.UserModules.Select(x => new UserModuleDto(x));
        Roles = entity.Type.GetUserPrivileges();
    }

    public Guid Id { get; }

    public IEnumerable<UserModuleDto> Modules { get; set; }

    public IEnumerable<UserType> Roles { get; set; }

    public string Token { get; }

    public string Username { get; }
}