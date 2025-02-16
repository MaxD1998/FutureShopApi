using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.ProductBase.Commands;
using Shop.Core.Cqrs.ProductBase.Queries;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;

namespace Api.Modules.Shop.Controllers;

public class ProductBaseController : ShopModuleBaseController
{
    public ProductBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductBaseFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductBaseIdNameDtoByIdQuery(id), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageProductBaseListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateProductBaseFormDtoCommand(id, dto), cancellationToken);
}