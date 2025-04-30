using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.User;
using Authorization.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Authorization.Controllers;

public class AuthController : AuthModuleBaseController
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService,
        IFluentValidatorFactory fluentValidatorFactory,
        IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
        _authService = authService;
    }

    [HttpGet("RefreshToken")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GenerateTokenAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.RefreshTokenAsync, cancellationToken);

    [HttpPost("Login")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> LoginAsync([FromBody] LoginFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.LoginAsync, dto, cancellationToken);

    [HttpGet("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.LogoutAsync, cancellationToken);

    [HttpPost("Register")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    public Task<IActionResult> RegisterAsync([FromBody] UserFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_authService.RegisterAsync, dto, cancellationToken);
}