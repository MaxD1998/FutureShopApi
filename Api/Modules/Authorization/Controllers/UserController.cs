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

    [HttpDelete("Own/{id:guid}")]
    [Role(UserType.Customer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteOwnAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.DeleteOwnAsync, id, cancellationToken);

    [HttpGet("Own/{id:guid}")]
    [Role(UserType.Customer)]
    [Permission(AuthorizationPermission.UserRead)]
    [ProducesResponseType(typeof(UserDetailsResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GeOwntByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.GetOwnByIdAsync, id, cancellationToken);

    [HttpPut("Own/{id:guid}")]
    [Role(UserType.Customer)]
    [ProducesResponseType(typeof(UserDetailsResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateOwnAsync([FromRoute] Guid id, [FromBody] UserDetailsRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.UpdateOwnAsync, id, dto, cancellationToken);

    [HttpPatch("Own/Password/{id:guid}")]
    [Role(UserType.Customer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateOwnPasswordAsync([FromRoute] Guid id, [FromBody] UserPasswordFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userService.UpdateOwnPasswordAsync, id, dto, cancellationToken);

    #endregion For Customers
}