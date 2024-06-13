using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductBase.Commands;
using Product.Core.Dtos.ProductBase;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class ProductBaseController : BaseController
{
    public ProductBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateBaseAsync([FromBody] ProductBaseFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateProductBaseFormDtoCommand(dto), cancellationToken);
}