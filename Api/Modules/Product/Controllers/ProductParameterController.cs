using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductParameter.Queries;
using Product.Core.Dtos;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class ProductParameterController : BaseController
{
    public ProductParameterController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("ProductBaseId/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByParameterBaseIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsProductParameterIdNameDtoByParameterBaseIdQuery(id), cancellationToken);
}