using Authorization.Core.Dtos;
using Authorization.Core.Dtos.PermissionGroup;
using Authorization.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;

namespace Api.Modules.Authorization.Controllers;

[Role(UserType.Employee)]
public class PermissionGroupController(IPermissionGroupService permissionGroupService) : AuthModuleBaseController
{
    private readonly IPermissionGroupService _permissionGroupService = permissionGroupService;

    [HttpPost]
    [Permission(AuthorizationPermission.PermissionGroupAddUpdate)]
    [ProducesResponseType(typeof(PermissionGroupResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] PermissionGroupRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Permission(AuthorizationPermission.PermissionGroupDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Permission(AuthorizationPermission.PermissionGroupRead)]
    [ProducesResponseType(typeof(PermissionGroupResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.GetByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [Role(UserType.Employee)]
    [Permission(AuthorizationPermission.PermissionGroupRead, AuthorizationPermission.UserRead)]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync([FromQuery] List<Guid> excludedIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.GetListIdNameAsync, excludedIds, cancellationToken);

    [HttpGet("Page")]
    [Permission(AuthorizationPermission.PermissionGroupRead)]
    [ProducesResponseType(typeof(PageDto<PermissionGroupListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(AuthorizationPermission.PermissionGroupAddUpdate)]
    [ProducesResponseType(typeof(PermissionGroupResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PermissionGroupRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_permissionGroupService.UpdateAsync, id, dto, cancellationToken);
}