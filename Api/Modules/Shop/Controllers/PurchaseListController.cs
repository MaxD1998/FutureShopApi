using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.PurchaseList;
using Shop.Core.Interfaces.Services;

namespace Api.Modules.Shop.Controllers;

public class PurchaseListController(IPurchaseListService purchaseListService) : ShopModuleBaseController
{
    private readonly IPurchaseListService _purchaseListService = purchaseListService;

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseListResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] PurchaseListRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PurchaseListDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.GetByIdAsync, id, cancellationToken);

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<PurchaseListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListByUserIdFromJwtAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.GetListByAuthorizedUserAsync, cancellationToken);

    [HttpPost("ImportBasket")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public Task<IActionResult> ImportPurchaseList([FromBody] ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.ImportBasketAsync, dto, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PurchaseListResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PurchaseListRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_purchaseListService.UpdateAsync, id, dto, cancellationToken);
}