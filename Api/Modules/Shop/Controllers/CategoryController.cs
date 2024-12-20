using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.Category.Queries;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;

namespace Api.Modules.Shop.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpGet("IdName/{id:guid}")]
    [ProducesResponseType(typeof(IdNameDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetIdNameByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetCategoryIdNameDtoByIdQuery(id), cancellationToken);

    [HttpGet("CategoryParentId")]
    [ProducesResponseType(typeof(IEnumerable<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetListByCategoryParentIdAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryListDtoByCategoryParentQuery(), cancellationToken);

    [HttpGet("CategoryParentId/{categoryParentId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetListByCategoryParentIdAsync([FromRoute] Guid categoryParentId, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryListDtoByCategoryParentQuery(categoryParentId), cancellationToken);
}