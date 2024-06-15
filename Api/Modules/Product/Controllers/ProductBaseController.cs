using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductBase.Commands;
using Product.Core.Cqrs.ProductBase.Queries;
using Product.Core.Dtos.ProductBase;
using Shared.Api.Bases;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class ProductBaseController : BaseController
{
    public ProductBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateProductBaseFormDtoCommand(dto), cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteProductBaseCommand(id), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductBaseFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductBaseDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageProductBaseDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateProductBaseFormDtoCommand(id, dto), cancellationToken);
}