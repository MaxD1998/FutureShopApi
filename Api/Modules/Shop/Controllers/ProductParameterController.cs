using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;
using Shop.Core.Cqrs.ProductParameter.Queries;
using Shop.Core.Dtos.ProductParameter;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.User)]
public class ProductParameterController : ShopModuleBaseController
{
    public ProductParameterController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("ProductId/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ProductParameterFlatDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByProductIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListProductParameterIdNameDtoByProductIdQuery(id), cancellationToken);
}