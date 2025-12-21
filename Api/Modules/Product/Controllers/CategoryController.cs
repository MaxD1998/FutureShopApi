using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Product.Core.Services;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Employee)]
public class CategoryController(ICategoryService categoryService) : ProductModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    [Permission(ProductPermission.CategoryAddUpdate)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> CreateAsync([FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Permission(ProductPermission.CategoryDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("{id:guid}")]
    [Permission(ProductPermission.CategoryRead)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetByIdAsync, id, cancellationToken);

    [HttpGet("All")]
    [Permission(ProductPermission.CategoryRead, ProductPermission.ProductBaseRead)]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListIdNameAsync, cancellationToken);

    [HttpGet("PotentialParentCategories/{id:guid?}")]
    [Permission(ProductPermission.CategoryRead)]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListPotentialParentCategories([FromRoute] Guid? id, [FromQuery] List<Guid> childIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListPotentialParentCategories, id, childIds, cancellationToken);

    [HttpGet("PotentialSubcategories/{id:guid?}")]
    [Permission(ProductPermission.CategoryRead)]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetListPotentialSubcategoriesAsync([FromRoute] Guid? id, [FromQuery] Guid? parentId, [FromQuery] List<Guid> childIds, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetListPotentialSubcategoriesAsync, id, parentId, childIds, cancellationToken);

    [HttpGet("Page")]
    [Permission(ProductPermission.CategoryRead)]
    [ProducesResponseType(typeof(PageDto<CategoryListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ProductPermission.CategoryAddUpdate)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.UpdateAsync, id, dto, cancellationToken);
}