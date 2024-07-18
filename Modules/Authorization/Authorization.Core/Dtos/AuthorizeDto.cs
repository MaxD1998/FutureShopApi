using Authorization.Domain.Entities;
using Shared.Domain.Enums;

namespace Authorization.Core.Dtos;

public class AuthorizeDto
{
    public AuthorizeDto(UserEntity entity, string token)
    {
        Id = entity.Id;
        Username = $"{entity.FirstName} {entity.LastName}";
        Token = token;
        Roles = entity.Type.GetUserPrivileges();
    }

    public Guid Id { get; }

    public IEnumerable<UserType> Roles { get; set; }

    public string Token { get; }

    public string Username { get; }
}