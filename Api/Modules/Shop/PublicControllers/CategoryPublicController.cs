using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Services;

namespace Api.Modules.Shop.PublicControllers;

public class CategoryPublicController(ICategoryService categoryService) : ShopModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("Active/IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActiveIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveIdNameByIdAsync, id, cancellationToken);

    [HttpGet("Active/List")]
    [ProducesResponseType(typeof(List<CategoryListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActiveListAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveListAsync, cancellationToken);
}