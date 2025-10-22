using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class AdCampaignController(IAdCampaignService adCampaignService) : ShopModuleBaseController
{
    private readonly IAdCampaignService _adCampaignService = adCampaignService;

    [HttpPost]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] AdCampaignRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("Actual")]
    [ProducesResponseType(typeof(List<IdFileIdDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<IActionResult> GetActualAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetActualAsync, cancellationToken);

    [HttpGet("ActualById/{id:guid}")]
    [ProducesResponseType(typeof(IdFileIdDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<IActionResult> GetActualByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetActualByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [ProducesResponseType(typeof(List<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetListIdNameAsync, cancellationToken);

    [HttpGet("Page")]
    [ProducesResponseType(typeof(PageDto<AdCampaignListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] AdCampaignRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.UpdateAsync, id, dto, cancellationToken);
}