using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.ProductPhoto.Commands;
using Product.Core.Cqrs.ProductPhoto.Queries;
using Product.Core.Dtos.ProductBase;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product.Controllers;

public class ProductPhotoController : BaseController
{
    public ProductPhotoController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductBaseFormDto), StatusCodes.Status200OK)]
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
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        => await ApiFileResponseAsync(new GetProductPhotoDocumentByIdQuery(id), cancellationToken);
}