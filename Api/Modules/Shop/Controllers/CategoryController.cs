using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Domain.Enums;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Interfaces.Services;

namespace Api.Modules.Shop.Controllers;

public class CategoryController(ICategoryService categoryService) : ShopModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    #region For public

    [HttpGet("Active/IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActiveIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveIdNameByIdAsync, id, cancellationToken);

    [HttpGet("Active/List")]
    [ProducesResponseType(typeof(List<CategoryListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetActiveListAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveListAsync, cancellationToken);

    #endregion For public

    #region For Employee

    [HttpGet("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.CategoryRead)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdName/{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.CategoryRead, ShopPermission.ProductBaseRead)]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetIdNameByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.CategoryRead)]
    [ProducesResponseType(typeof(PageDto<CategoryPageListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Role(UserType.Employee)]
    [Permission(ShopPermission.CategoryAddUpdate)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.UdpateAsync, id, dto, cancellationToken);

    #endregion For Employee
}