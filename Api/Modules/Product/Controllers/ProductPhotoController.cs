using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductPhoto.Commands;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class ProductPhotoController : BaseController
{
    public ProductPhotoController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost("Photo/Upload/")]
    public async Task<IActionResult> CreatePhoto(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateListProductPhotoCommand(files), cancellationToken);
}