using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Employee)]
public class AdCampaignController(IAdCampaignService adCampaignService) : ShopModuleBaseController
{
    private readonly IAdCampaignService _adCampaignService = adCampaignService;

    [HttpPost]
    [Permission(ShopPermission.AdCamaignAddUpdate)]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] AdCampaignRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Permission(ShopPermission.AdCamaignDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Permission(ShopPermission.AdCamaignRead)]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [Permission(ShopPermission.AdCamaignRead, ShopPermission.PromotionRead)]
    [ProducesResponseType(typeof(List<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetListIdNameAsync, cancellationToken);

    [HttpGet("Page")]
    [Permission(ShopPermission.AdCamaignRead)]
    [ProducesResponseType(typeof(PageDto<AdCampaignListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ShopPermission.AdCamaignAddUpdate)]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] AdCampaignRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.UpdateAsync, id, dto, cancellationToken);
}