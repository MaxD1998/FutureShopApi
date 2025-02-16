using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.Basket.Commands;
using Shop.Core.Cqrs.Basket.Queries;
using Shop.Core.Dtos.Basket;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class BasketController : ShopModuleBaseController
{
    private readonly IBasketSerivce _basketSerivce;

    public BasketController(IBasketSerivce basketSerivce, IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
        _basketSerivce = basketSerivce;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] BasketFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateBasketFormDtoCommand(dto), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetBasketDtoByIdQuery(id, favouriteId), cancellationToken);

    [HttpGet("UserBasket")]
    [Authorize]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserBasketAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetBasketDtoByUserIdFromJwtQuery(), cancellationToken);

    [HttpPost("ImportPurchaseList")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportPurchaseList([FromBody] ImportPurchaseListToBasketDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, () => _basketSerivce.ImportPurchaseListAsync(dto, cancellationToken), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BasketFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] BasketFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateBasketFormDtoCommand(id, dto), cancellationToken);
}