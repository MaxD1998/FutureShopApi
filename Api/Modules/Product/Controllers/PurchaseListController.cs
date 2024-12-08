using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.PurchaseList.Commands;
using Product.Core.Cqrs.PurchaseList.Queries;
using Product.Core.Dtos.Basket;
using Product.Core.Dtos.PurchaseList;
using Product.Core.Services;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class PurchaseListController : BaseController
{
    private readonly IPurchaseListService _purchaseListService;

    public PurchaseListController(IPurchaseListService purchaseListService, IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
        _purchaseListService = purchaseListService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseListFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] PurchaseListFormDto dto, CancellationToken cancellation = default)
        => await ApiResponseAsync(dto, new CreatePurchaseListFormDtoCommand(dto), cancellation);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<PurchaseListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellation = default)
        => await ApiResponseAsync(new DeletePurchaseListByIdCommand(id), cancellation);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PurchaseListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellation = default)
        => await ApiResponseAsync(new GetPurchaseListDtoByIdQuery(id), cancellation);

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<PurchaseListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByUserIdFromJwtAsync(CancellationToken cancellation = default)
        => await ApiResponseAsync(new GetListPurchaseListDtoByUserIdFromJwtQuery(), cancellation);

    [HttpPost("ImportBasket")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportPurchaseList([FromBody] ImportBasketToPurchaseListDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, () => _purchaseListService.ImportBasketAsync(dto, cancellationToken), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PurchaseListFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PurchaseListFormDto dto, CancellationToken cancellation = default)
        => await ApiResponseAsync(dto, new UpdatePurchaseListFormDtoCommand(id, dto), cancellation);
}