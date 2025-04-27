using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.Category.Commands;
using Shop.Core.Cqrs.Category.Queries;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;

namespace Api.Modules.Shop.Controllers;

public class CategoryController : ShopModuleBaseController
{
    public CategoryController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetCategoryFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetCategoryIdNameDtoByIdQuery(id), cancellationToken);

    [HttpGet("List")]
    [ProducesResponseType(typeof(IEnumerable<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetListByCategoryParentIdAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryListDtoQuery(), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<CategoryPageListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageCategoryPageListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateCategoryFormDtoCommand(id, dto), cancellationToken);
}