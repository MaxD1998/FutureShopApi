using Microsoft.AspNetCore.Mvc;
using Shared.Api.Attributes;
using Shared.Infrastructure.Enums;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shared.Shared.Enums;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

[Role(UserType.Employee)]
public class CategoryController(ICategoryService categoryService) : ShopModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("{id:guid}")]
    [Permission(ShopPermission.CategoryRead)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdName/{id:guid}")]
    [Permission(ShopPermission.CategoryRead, ShopPermission.ProductBaseRead)]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetIdNameByIdAsync, id, cancellationToken);

    [HttpGet("Page")]
    [Permission(ShopPermission.CategoryRead)]
    [ProducesResponseType(typeof(PageDto<CategoryPageListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetPageListAsync, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    [Permission(ShopPermission.CategoryAddUpdate)]
    [ProducesResponseType(typeof(CategoryResponseFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryRequestFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.UdpateAsync, id, dto, cancellationToken);
}