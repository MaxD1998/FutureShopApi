using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.Product.Commands;
using Shop.Core.Cqrs.Product.Queries;
using Shop.Core.Dtos.Product;

namespace Api.Modules.Shop.Controllers;

public class ProductController : ShopModuleBaseController
{
    public ProductController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductDtoByIdQuery(id, favouriteId), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageProductListDtoQuery(pageNumber), cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListProductShopListDtoByCategoryIdQuery(categoryId, request, favouriteId), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateProductFormDtoCommand(id, dto), cancellationToken);
}