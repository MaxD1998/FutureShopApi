using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductBaseController(IProductBaseService productBaseService) : ShopModuleBaseController
{
    private readonly IProductBaseService _productBaseService = productBaseService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetIdNametByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetPageAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.UpdateAsync, id, dto, cancellationToken);
}