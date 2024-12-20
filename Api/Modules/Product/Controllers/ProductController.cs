using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.Product.Commands;
using Product.Core.Cqrs.Product.Queries;
using Product.Core.Dtos.Product;
using Shared.Api.Attributes;
using Shared.Api.Bases;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Manager)]
public class ProductController : BaseController
{
    public ProductController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateProductFormDtoCommand(dto), cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteProductByIdCommand(id), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageProductListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateProductFormDtoCommand(id, dto), cancellationToken);
}