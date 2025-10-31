using File.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.File.PublicController;

public class FilePublicController(IFileService fileService) : FileModuleBaseController
{
    private readonly IFileService _fileService = fileService;

    /// <summary>
    /// It returns a file by his unique id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        => ApiFileResponseAsync(_fileService.GetByIdAsync, id, cancellationToken);
}