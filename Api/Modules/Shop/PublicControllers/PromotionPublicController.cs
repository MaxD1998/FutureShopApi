using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;

namespace Api.Modules.Shop.PublicControllers;

public class PromotionPublicController(IPromotionService promotionService) : ShopModuleBaseController
{
    private readonly IPromotionService _promotionService = promotionService;

    [HttpGet("ActualCodes")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActualCodesAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.GetActualCodesAsync, cancellationToken);
}