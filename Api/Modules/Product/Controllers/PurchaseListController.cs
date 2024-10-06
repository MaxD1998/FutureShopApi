using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.PurchaseList.Commands;
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
    public async Task<IActionResult> CreateAsync([FromBody] PurchaseListFormDto dto)
        => await ApiResponseAsync(dto, new CreatePurchaseListFormDtoCommand(dto));

    [HttpGet]
    [ProducesResponseType(typeof(PurchaseListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserIdFromAccessor()
    {
    }
}