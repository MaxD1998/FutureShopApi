using Authorization.Domain.Entities;

namespace Authorization.Core.Dtos;

public class AuthorizeDto
{
    public AuthorizeDto(UserEntity entity, string token)
    {
        Id = entity.Id;
        Username = $"{entity.FirstName} {entity.LastName}";
        Token = token;
    }

    public Guid Id { get; }

    public string Token { get; }

    public string Username { get; }
}