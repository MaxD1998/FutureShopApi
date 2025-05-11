using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shop.Core.Dtos.ProductParameter;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.User)]
public class ProductParameterController(IProductParameterService productParameterService) : ShopModuleBaseController
{
    private readonly IProductParameterService _productParameterService = productParameterService;

    [HttpGet("ProductId/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductParameterFlatDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByProductIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(_productParameterService.GetListByProductIdAsync, id, cancellationToken);
}