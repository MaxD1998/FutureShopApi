using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Product.Core.Services;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;

namespace Api.Modules.Product.Controllers;

[Role(UserType.User)]
public class CategoryController(ICategoryService categoryService) : ProductModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListIdNameAsync, cancellationToken);

    [HttpGet("PotentialParentCategories/{id:guid?}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListPotentialParentCategories([FromRoute] Guid? id, [FromQuery] List<Guid> childIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListPotentialParentCategories, id, childIds, cancellationToken);

    [HttpGet("PotentialSubcategories/{id:guid?}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListPotentialSubcategoriesAsync([FromRoute] Guid? id, [FromQuery] Guid? parentId, [FromQuery] List<Guid> childIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListPotentialSubcategoriesAsync, id, parentId, childIds, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<CategoryListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetPageListAsync, pageNumber, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.UpdateAsync, id, dto, cancellationToken);
}