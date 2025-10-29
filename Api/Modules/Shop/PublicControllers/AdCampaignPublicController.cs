using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos;
using Shop.Core.Services;

namespace Api.Modules.Shop.PublicControllers;

public class AdCampaignPublicController(IAdCampaignService adCampaignService) : ShopModuleBaseController
{
    private readonly IAdCampaignService _adCampaignService = adCampaignService;

    [HttpGet("Actual")]
    [ProducesResponseType(typeof(List<IdFileIdDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActualAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetActualAsync, cancellationToken);

    [HttpGet("ActualById/{id:guid}")]
    [ProducesResponseType(typeof(IdFileIdDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActualByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_adCampaignService.GetActualByIdAsync, id, cancellationToken);
}