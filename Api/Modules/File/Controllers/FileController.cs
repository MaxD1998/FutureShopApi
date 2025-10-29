using File.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.ProductPhoto;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;

namespace Api.Modules.File.Controllers;

[Role(UserType.Employee)]
public class FileController(IFileService fileService) : FileModuleBaseController
{
    private readonly IFileService _fileService = fileService;

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateListAsync([FromForm] IFormFileCollection files, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_fileService.CreateListAsync, files, cancellationToken);

    [HttpGet("Info/")]
    [ProducesResponseType(typeof(IEnumerable<ProductPhotoInfoDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListInfoByIdsAsync([FromQuery] List<string> ids, CancellationToken cancellationToken)
        => ApiResponseAsync(_fileService.GetListInfoByIdsAsync, ids, cancellationToken);
}