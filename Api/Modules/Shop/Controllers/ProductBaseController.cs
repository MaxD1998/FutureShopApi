using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductBaseController(IProductBaseService productBaseService) : ShopModuleBaseController
{
    private readonly IProductBaseService _productBaseService = productBaseService;

    #region For Employee

    [HttpGet("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductBaseAddUpdate)]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductBaseRead, ShopPermission.ProductRead)]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetIdNametByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductBaseRead)]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.ProductBaseAddUpdate)]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.UpdateAsync, id, dto, cancellationToken);

    #endregion For Employee
}