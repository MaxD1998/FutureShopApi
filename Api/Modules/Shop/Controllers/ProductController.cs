using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Dtos.Product;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductController(IProductService productService) : ShopModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetDetailsByIdAsync, id, favouriteId, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pageNumber, cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetShopListByCategoryIdAsync, categoryId, request, cancellationToken);

    [HttpPost("SimulateAddPrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateAddPriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateAddPrice, request, cancellationToken);

    [HttpPost("SimulateRemovePrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateRemovePriceAsync([FromBody] SimulateRemovePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateRemovePrice, request, cancellationToken);

    [HttpPost("SimulateUpdatePrice")]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateUpdatePriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateUpdatePrice, request, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);
}