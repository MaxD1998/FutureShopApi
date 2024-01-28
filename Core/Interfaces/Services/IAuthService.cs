using Core.Dtos;
using Core.Dtos.Login;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    Task<AuthorizeDto> LoginAsync(LoginDto dto);

    Task LogoutAsync();

    Task<AuthorizeDto> RefreshTokenAsync();
}