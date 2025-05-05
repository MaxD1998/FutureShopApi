using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductBaseController(IProductBaseService productBaseService) : ShopModuleBaseController
{
    private readonly IProductBaseService _productBaseService = productBaseService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetIdNametByIdAsync, id, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetPageAsync, pageNumber, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.UpdateAsync, id, dto, cancellationToken);
}