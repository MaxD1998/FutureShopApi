using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.Category.Commands;
using Product.Core.Cqrs.Category.Queries;
using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Shared.Api.Attributes;
using Shared.Api.Bases;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shared.Domain.Enums;

namespace Api.Modules.Product.Controllers;

[Role(UserType.Manager)]
public class CategoryController : BaseController
{
    public CategoryController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new CreateCategoryFormDtoCommand(dto), cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteCategoryByIdCommand(id), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetCategoryFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("AvailableToBeChild")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAvailableToBeChildAsync([FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryIdNameDtoAvailableToBeChildCategoryQuery(null, parentId, childIds), cancellationToken);

    [HttpGet("AvailableToBeChild/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAvailableToBeChildAsync([FromRoute] Guid id, [FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryIdNameDtoAvailableToBeChildCategoryQuery(id, parentId, childIds), cancellationToken);

    [HttpGet("AvailableToBeParent")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAvailableToBeParentAsync([FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryIdNameDtoAvailableToBeParentCategoryQuery(null, childIds), cancellationToken);

    [HttpGet("AvailableToBeParent/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAvailableToBeParentAsync([FromRoute] Guid id, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryIdNameDtoAvailableToBeParentCategoryQuery(id, childIds), cancellationToken);

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

    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListIdNameAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetListCategoryIdNameDtoQuery(), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<CategoryListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageCategoryListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateCategoryFormDtoCommand(id, dto), cancellationToken);
}