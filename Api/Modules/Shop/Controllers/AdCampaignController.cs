using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos.AdCampaign;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class AdCampaignController(IAdCampaignService adCampaignService) : ShopModuleBaseController
{
    private readonly IAdCampaignService _adCampaignService = adCampaignService;

    [HttpPost]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] AdCampaignFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("Actual")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<IActionResult> GetActualAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetActualAsync, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<AdCampaignListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetPageAsync, pageNumber, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] AdCampaignFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.UpdateAsync, id, dto, cancellationToken);
}