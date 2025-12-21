using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.Register;
using Shared.Core.Dtos;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Authorization.Core.Interfaces.Services;

public interface IAuthService
{
    Task<ResultDto<AuthorizeDto>> LoginAsync(LoginFormDto dto, CancellationToken cancellationToken = default);

    Task<ResultDto> LogoutAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<ResultDto<AuthorizeDto>> RegisterAsync(RegisterFormDto dto, CancellationToken cancellationToken = default);
}
