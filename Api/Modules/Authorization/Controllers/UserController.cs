using Authorization.Core.Dtos.User;
using Authorization.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;

namespace Api.Modules.Authorization.Controllers;

public class UserController(IUserService userService) : AuthModuleBaseController
{
    private readonly IUserService _userService = userService;

    #region For Employee

    [HttpPost]
    [Role(UserType.Employee)]
    [Permission(AuthorizationPermission.UserAddUpdate)]
    [ProducesResponseType(typeof(UserResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] UserCreateRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(AuthorizationPermission.UserDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.DeleteAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(AuthorizationPermission.UserRead)]
    [ProducesResponseType(typeof(UserResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [Permission(AuthorizationPermission.UserRead)]
    [ProducesResponseType(typeof(PageDto<UserListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(AuthorizationPermission.UserAddUpdate)]
    [ProducesResponseType(typeof(UserResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserUpdateRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.UpdateAsync, id, dto, cancellationToken);

    #endregion For Employee

    #region For Customers

    [HttpDelete("Own/")]
    [Role(UserType.Customer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteOwnAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.DeleteOwnAsync, cancellationToken);

    [HttpPatch("Own/Password/")]
    [Role(UserType.Customer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateOwnPasswordAsync([FromBody] UserPasswordFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.UpdateOwnPasswordAsync, dto, cancellationToken);

    #endregion For Customers
}