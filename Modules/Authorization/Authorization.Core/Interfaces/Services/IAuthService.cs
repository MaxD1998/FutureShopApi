using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.User;

namespace Authorization.Core.Interfaces.Services;

public interface IAuthService
{
    Task<AuthorizeDto> LoginAsync(LoginDto dto);

    Task LogoutAsync();

    Task<AuthorizeDto> RefreshTokenAsync();

    Task<AuthorizeDto> RegisterAsync(UserFormDto dto);
}