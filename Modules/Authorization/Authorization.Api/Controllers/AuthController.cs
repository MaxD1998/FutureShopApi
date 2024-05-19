using Authorization.Core.Dtos;
using Authorization.Core.Dtos.Login;
using Authorization.Core.Dtos.User;
using Authorization.Core.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Authorization.Api.Controllers;

public class AuthController : BaseController
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
    [AllowAnonymous]
    public async Task<IActionResult> GenerateTokenAsync()
        => await ApiResponseAsync(() => _authService.RefreshTokenAsync());

    [HttpPost("Login")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        => await ApiResponseAsync(dto, () => _authService.LoginAsync(dto));

    [HttpGet("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout()
        => await ApiResponseAsync(_authService.LogoutAsync);

    [HttpPost("Register")]
    [ProducesResponseType(typeof(AuthorizeDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] UserFormDto dto)
        => await ApiResponseAsync(dto, () => _authService.RegisterAsync(dto));
}