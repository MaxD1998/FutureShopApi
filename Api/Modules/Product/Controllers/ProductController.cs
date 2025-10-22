using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Product;
using Product.Core.Services;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;

namespace Api.Modules.Product.Controllers;

[Role(UserType.User)]
public class ProductController(IProductService productService) : ProductModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] ProductRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);
}