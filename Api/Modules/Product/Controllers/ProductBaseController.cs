using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductBase.Commands;
using Product.Core.Cqrs.ProductBase.Queries;
using Product.Core.Dtos;
using Product.Core.Dtos.ProductBase;
using Shared.Api.Attributes;
using Shared.Api.Bases;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Manager)]
public class ProductBaseController : BaseController
{
    public ProductBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateProductBaseFormDtoCommand(dto), cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteProductBaseCommand(id), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductBaseFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductBaseIdNameDtoByIdQuery(id), cancellationToken);

    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListProductBaseIdNameDtoQuery(), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageProductBaseListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateProductBaseFormDtoCommand(id, dto), cancellationToken);
}