using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Promotion;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class PromotionController(IPromotionService promotionService) : ShopModuleBaseController
{
    private readonly IPromotionService _promotionService = promotionService;

    [HttpPost]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] PromotionRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("ActualCodes")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActualCodesAsync(CancellationToken cancellationToken = default)
    => ApiResponseAsync(_promotionService.GetActualCodesAsync, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [ProducesResponseType(typeof(List<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
    => ApiResponseAsync(_promotionService.GetListIdNameAsync, cancellationToken);

    [HttpGet("Page")]
    [ProducesResponseType(typeof(PageDto<PromotionListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PromotionRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.UpdateAsync, id, dto, cancellationToken);
}