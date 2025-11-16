using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shop.Core.Dtos.User.UserDeliveryAddress;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Customer)]
public class UserDeliveryAddressController(IUserDeliveryAddressService userDeliveryAddressService) : ShopModuleBaseController
{
    private readonly IUserDeliveryAddressService _userDeliveryAddressService = userDeliveryAddressService;

    [HttpPost]
    [ProducesResponseType(typeof(UserDeliveryAddressResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userDeliveryAddressService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userDeliveryAddressService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{externalId:guid}")]
    [ProducesResponseType(typeof(UserDeliveryAddressResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByUserExternalIdAsync([FromRoute] Guid externalId, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userDeliveryAddressService.GetByUserExternalIdAsync, externalId, cancellationToken);

    [HttpGet("List/{externalId:guid}")]
    [ProducesResponseType(typeof(List<UserDeliveryAddressResponseFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListByUserExternalIdAsync([FromRoute] Guid externalId, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userDeliveryAddressService.GetListByUserExternalIdAsync, externalId, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UserDeliveryAddressResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserDeliveryAddressRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userDeliveryAddressService.UpdateAsync, id, dto, cancellationToken);
}