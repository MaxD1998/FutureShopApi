using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductPhoto.Commands;
using Product.Core.Cqrs.ProductPhoto.Queries;
using Product.Core.Dtos.ProductPhoto;
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

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateListAsync([FromForm] IFormFileCollection files, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateListProductPhotoCommand(files), cancellationToken);

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteProductPhotoByIdCommand(id), cancellationToken);

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteListAsync([FromQuery] IEnumerable<string> ids, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteListProductPhotoByIdCommand(ids), cancellationToken);

    /// <summary>
    /// It returns a file by his unique id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        => await ApiFileResponseAsync(new GetProductPhotoDocumentByIdQuery(id), cancellationToken);

    [HttpGet("Info/")]
    [ProducesResponseType(typeof(IEnumerable<ProductPhotoInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListInfoByIdsAsync([FromQuery] IEnumerable<string> ids, CancellationToken cancellationToken)
        => await ApiResponseAsync(new GetListProductPhotoInfoDtoByIdsQuery(ids), cancellationToken);
}