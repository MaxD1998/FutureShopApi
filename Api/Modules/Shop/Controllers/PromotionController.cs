using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Promotion;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Employee)]
public class PromotionController(IPromotionService promotionService) : ShopModuleBaseController
{
    private readonly IPromotionService _promotionService = promotionService;

    [HttpPost]
    [Permission(ShopPermission.PromotionAddUpdate)]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] PromotionRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Permission(ShopPermission.PromotionDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Permission(ShopPermission.PromotionRead)]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [Permission(ShopPermission.PromotionRead, ShopPermission.AdCamaignRead)]
    [ProducesResponseType(typeof(List<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
    => ApiResponseAsync(_promotionService.GetListIdNameAsync, cancellationToken);

    [HttpGet("Page")]
    [Permission(ShopPermission.PromotionRead)]
    [ProducesResponseType(typeof(PageDto<PromotionListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ShopPermission.PromotionAddUpdate)]
    [ProducesResponseType(typeof(PromotionResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PromotionRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_promotionService.UpdateAsync, id, dto, cancellationToken);
}