namespace Authorization.Core.Dtos;

public class AuthorizeDto
{
    public Guid Id { get; set; }

    public string Token { get; set; }

    public string Username { get; set; }
}