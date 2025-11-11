using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.Register;
using Authorization.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authorization.Controllers;

public class AuthorizationController(IAuthService authService) : AuthModuleBaseController
{
    private readonly IAuthService _authService = authService;

    [HttpGet("RefreshToken")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GenerateTokenAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.RefreshTokenAsync, cancellationToken);

    [HttpPost("Login")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> LoginAsync([FromBody] LoginFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.LoginAsync, dto, cancellationToken);

    [HttpGet("Logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.LogoutAsync, cancellationToken);

    [HttpPost("Register")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> RegisterAsync([FromBody] RegisterFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.RegisterAsync, dto, cancellationToken);
}