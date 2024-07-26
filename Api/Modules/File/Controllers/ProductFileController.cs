using File.Core.Cqrs.ProductPhoto.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.File.Controllers;

public class ProductFileController : BaseController
{
    public ProductFileController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost("Photo/Upload/{id:guid}")]
    public async Task<IActionResult> CreatePhoto([FromRoute] Guid id, [FromForm] IFormFile file, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateProductPhotoCommand(id, file), cancellationToken);
}