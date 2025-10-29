using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Services;

namespace Api.Modules.Shop.PublicControllers;

public class CategoryPublicController(ICategoryService categoryService) : ShopModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    => ApiResponseAsync(_categoryService.GetIdNameByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [ProducesResponseType(typeof(List<CategoryListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListByCategoryParentIdAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveListAsync, cancellationToken);
}