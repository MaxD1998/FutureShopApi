using File.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.ProductPhoto;

namespace Api.Modules.File.Controllers;

public class FileController(IFileService fileService) : FileModuleBaseController
{
    private readonly IFileService _fileService = fileService;

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateListAsync([FromForm] IFormFileCollection files, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_fileService.CreateListAsync, files, cancellationToken);

    /// <summary>
    /// It returns a file by his unique id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        => ApiFileResponseAsync(_fileService.GetByIdAsync, id, cancellationToken);

    [HttpGet("Info/")]
    [ProducesResponseType(typeof(IEnumerable<ProductPhotoInfoDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListInfoByIdsAsync([FromQuery] List<string> ids, CancellationToken cancellationToken)
        => ApiResponseAsync(_fileService.GetListInfoByIdsAsync, ids, cancellationToken);
}