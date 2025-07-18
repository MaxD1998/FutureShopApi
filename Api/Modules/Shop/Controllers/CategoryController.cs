﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class CategoryController(ICategoryService categoryService) : ShopModuleBaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetByIdAsync, id, cancellationToken);

    [HttpGet("IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetIdNameByIdAsync, id, cancellationToken);

    [HttpGet("List")]
    [ProducesResponseType(typeof(List<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<IActionResult> GetListByCategoryParentIdAsync(CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetActiveListAsync, cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<CategoryPageListDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.GetPageListAsync, pageNumber, cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryFormDto dto, CancellationToken cancellationToken = default)
        => ApiResponseAsync(_categoryService.UdpateAsync, id, dto, cancellationToken);
}