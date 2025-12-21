using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductController(IProductService productService) : ShopModuleBaseController
{
    private readonly IProductService _productService = productService;

    #region For public

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetDetailsByIdAsync, id, cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetShopListByCategoryIdAsync, categoryId, request, cancellationToken);

    #endregion For public

    #region For Employee

    [HttpGet("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductRead)]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductRead)]
    [ProducesResponseType(typeof(List<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync([FromQuery] List<Guid> excludedIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetListIdNameAsync, excludedIds, cancellationToken);

    [HttpGet("Page")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.GetPageListAsync, pagination, cancellationToken);

    [HttpPost("SimulateAddPrice")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateAddPriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateAddPriceAsync, request, cancellationToken);

    [HttpPost("SimulateRemovePrice")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateRemovePriceAsync([FromBody] SimulateRemovePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateRemovePriceAsync, request, cancellationToken);

    [HttpPost("SimulateUpdatePrice")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(List<SimulatePriceFormDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> SimulateUpdatePriceAsync([FromBody] SimulatePriceRequestDto request, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.SimulateUpdatePriceAsync, request, cancellationToken);

    [HttpPut("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductAddUpdate)]
    [ProducesResponseType(typeof(ProductResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productService.UpdateAsync, id, dto, cancellationToken);

    #endregion For Employee
}