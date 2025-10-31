using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Employee)]
public class ProductController(IProductService productService) : ShopModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id:guid}")]
    [Permission(ShopPermission.ProductRead)]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [Permission(ShopPermission.ProductRead)]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync([FromQuery] List<Guid> excludedIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetListIdNameAsync, excludedIds, cancellationToken);

    [HttpGet("Page")]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pagination, cancellationToken);

    [HttpPost("SimulateAddPrice")]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateAddPriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateAddPriceAsync, request, cancellationToken);

    [HttpPost("SimulateRemovePrice")]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateRemovePriceAsync([FromBody] SimulateRemovePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateRemovePriceAsync, request, cancellationToken);

    [HttpPost("SimulateUpdatePrice")]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateUpdatePriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateUpdatePriceAsync, request, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);
}