using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.PurchaseList.Commands;
using Product.Core.Cqrs.PurchaseList.Queries;
using Product.Core.Dtos.PurchaseList;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class PurchaseListController : BaseController
{
    public PurchaseListController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseListFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] PurchaseListFormDto dto, CancellationToken cancellation = default)
        => await ApiResponseAsync(dto, new CreatePurchaseListFormDtoWithUserIdFromJwtCommand(dto), cancellation);

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PurchaseListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserIdFromJwtAsync(CancellationToken cancellation = default)
        => await ApiResponseAsync(new GetListPurchaseListDtoByUserIdFromJwtQuery(), cancellation);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PurchaseListFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] PurchaseListFormDto dto, CancellationToken cancellation = default)
        => await ApiResponseAsync(dto, new UpdatePurchaseListFormDtoWithUserIdFromJwtCommand(id, dto), cancellation);
}