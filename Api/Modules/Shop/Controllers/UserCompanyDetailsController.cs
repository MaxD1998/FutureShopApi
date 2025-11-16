using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shop.Core.Dtos.User.UserCompanyDetails;
using Shop.Core.Dtos.User.UserDeliveryAddress;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Customer)]
public class UserCompanyDetailsController(IUserCompanyDetailsService userCompanyDetailsService) : ShopModuleBaseController
{
    private readonly IUserCompanyDetailsService _userCompanyDetailsService = userCompanyDetailsService;

    [HttpPost]
    [ProducesResponseType(typeof(UserCompanyDetailsResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userCompanyDetailsService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userCompanyDetailsService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{externalId:guid}")]
    [ProducesResponseType(typeof(UserCompanyDetailsResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByUserExternalIdAsync([FromRoute] Guid externalId, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userCompanyDetailsService.GetByUserExternalIdAsync, externalId, cancellationToken);

    [HttpGet("List/{externalId:guid}")]
    [ProducesResponseType(typeof(List<UserCompanyDetailsResponseFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListByUserExternalIdAsync([FromRoute] Guid externalId, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userCompanyDetailsService.GetListByUserExternalIdAsync, externalId, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UserDeliveryAddressResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserCompanyDetailsRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_userCompanyDetailsService.UpdateAsync, id, dto, cancellationToken);
}