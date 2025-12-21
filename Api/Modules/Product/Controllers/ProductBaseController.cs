using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos;
using Product.Core.Dtos.ProductBase;
using Product.Core.Interfaces.Services;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Employee)]
public class ProductBaseController(IProductBaseService productBaseService) : ProductModuleBaseController
{
    private readonly IProductBaseService _productBaseService = productBaseService;

    [HttpPost]
    [Permission(ProductPermission.ProductBaseAddUpdate)]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] ProductBaseRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Permission(ProductPermission.ProductBaseDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.DeleteAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Permission(ProductPermission.ProductBaseRead)]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdNameById/{id:guid}")]
    [Permission(ProductPermission.ProductBaseRead, ProductPermission.ProductRead)]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetIdNameByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [Permission(ProductPermission.ProductBaseRead, ProductPermission.ProductRead)]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetListIdNameAsync, cancellationToken);

    [HttpGet("Page")]
    [Permission(ProductPermission.ProductDelete)]
    [ProducesResponseType(typeof(PageDto<ProductBaseListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ProductPermission.ProductBaseAddUpdate)]
    [ProducesResponseType(typeof(ProductBaseResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductBaseRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_productBaseService.UpdateAsync, id, dto, cancellationToken);
}