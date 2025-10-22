using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductController(IProductService productService) : ShopModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetDetailsByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync([FromQuery] List<Guid> excludedIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetListIdNameAsync, excludedIds, cancellationToken);

    [HttpGet("Page")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pagination, cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetShopListByCategoryIdAsync, categoryId, request, cancellationToken);

    [HttpPost("SimulateAddPrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateAddPriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateAddPriceAsync, request, cancellationToken);

    [HttpPost("SimulateRemovePrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateRemovePriceAsync([FromBody] SimulateRemovePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateRemovePriceAsync, request, cancellationToken);

    [HttpPost("SimulateUpdatePrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateUpdatePriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateUpdatePriceAsync, request, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);
}