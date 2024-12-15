using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;
using Shop.Core.Cqrs.ProductParameter.Queries;
using Shop.Core.Dtos;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Manager)]
public class ProductParameterController : ShopModuleBaseController
{
    public ProductParameterController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("ProductBaseId/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByParameterBaseIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListProductParameterIdNameDtoByParameterBaseIdQuery(id), cancellationToken);
}