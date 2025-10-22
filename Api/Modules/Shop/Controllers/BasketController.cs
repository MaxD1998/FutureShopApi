using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos.Basket;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class BasketController(IBasketSerivce basketSerivce) : ShopModuleBaseController
{
    private readonly IBasketSerivce _basketSerivce = basketSerivce;

    [HttpPost]
    [ProducesResponseType(typeof(BasketResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] BasketRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_basketSerivce.CreateAsync, dto, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_basketSerivce.GetByIdAsync, id, cancellationToken);

    [HttpGet("UserBasket")]
    [Authorize]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetUserBasketAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_basketSerivce.GetByAuthorizedUserAsync, cancellationToken);

    [HttpPost("ImportPurchaseList")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public Task<IActionResult> ImportPurchaseList([FromBody] ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_basketSerivce.ImportPurchaseListAsync, dto, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BasketResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] BasketRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_basketSerivce.UpdateAsync, id, dto, cancellationToken);
}