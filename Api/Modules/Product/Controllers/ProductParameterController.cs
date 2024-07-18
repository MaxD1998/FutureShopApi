using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductParameter.Queries;
using Product.Core.Dtos;
using Shared.Api.Attributes;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Manager)]
public class ProductParameterController : BaseController
{
    public ProductParameterController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("ProductBaseId/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsByParameterBaseIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsProductParameterIdNameDtoByParameterBaseIdQuery(id), cancellationToken);
}