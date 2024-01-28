using AutoMapper;
using Core.Dtos;
using Core.Dtos.Login;
using Core.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Bases;
using Shared.Factories.FluentValidation;

namespace Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService,
        IFluentValidatorFactory fluentValidatorFactory,
        IMapper mapper,
        IMediator mediator) : base(fluentValidatorFactory, mapper, mediator)
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

    [HttpDelete("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> Logout()
        => await ApiResponseAsync(_authService.LogoutAsync);
}