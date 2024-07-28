using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductPhoto.Commands;
using Shared.Api.Attributes;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Manager)]
public class ProductPhotoController : BaseController
{
    public ProductPhotoController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost("Photo/Upload/{id:guid}")]
    public async Task<IActionResult> CreatePhoto([FromRoute] Guid id, IFormFile file, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateProductPhotoCommand(id, file), cancellationToken);
}