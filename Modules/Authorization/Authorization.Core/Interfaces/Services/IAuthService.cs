using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.User;

namespace Authorization.Core.Interfaces.Services;

public interface IAuthService
{
    Task<AuthorizeDto> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default);

    Task LogoutAsync(CancellationToken cancellationToken = default);

    Task<AuthorizeDto> RefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<AuthorizeDto> RegisterAsync(UserFormDto dto, CancellationToken cancellationToken = default);
}