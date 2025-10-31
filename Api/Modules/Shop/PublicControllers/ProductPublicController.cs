using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos.Product;
using Shop.Core.Services;

namespace Api.Modules.Shop.PublicControllers;

public class ProductPublicController(IProductService productService) : ShopModuleBaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetDetailsByIdAsync, id, cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetShopListByCategoryIdAsync, categoryId, request, cancellationToken);
}