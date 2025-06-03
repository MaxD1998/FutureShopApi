using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Product;
using Product.Core.Services;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Infrastructure.Extensions;

namespace Api.Modules.Product.Controllers;

[Role(UserType.User)]
public class ProductController(IProductService productService) : ProductModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pageNumber, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);
}