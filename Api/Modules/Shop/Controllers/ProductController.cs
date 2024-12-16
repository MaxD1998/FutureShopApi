using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.Product.Queries;
using Shop.Core.Dtos.Product;

namespace Api.Modules.Shop.Controllers;

public class ProductController : BaseController
{
    public ProductController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("Details/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] Guid id, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetProductDtoByIdQuery(id, favouriteId), cancellationToken);

    [HttpGet("ShopList/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductShopListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShopListByCategoryIdAsync([FromRoute] Guid categoryId, [FromQuery] ProductShopListFilterRequestDto request, [FromQuery] Guid? favouriteId = null, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListProductShopListDtoByCategoryIdQuery(categoryId, request, favouriteId), cancellationToken);
}