using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;

namespace Authorization.Core.Interfaces.Services;

public interface IAuthService
{
    Task<AuthorizeDto> LoginAsync(LoginDto dto);

    Task LogoutAsync();

    Task<AuthorizeDto> RefreshTokenAsync();
}