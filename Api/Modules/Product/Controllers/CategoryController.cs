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

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<CategoryListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageCategoryListDtoQuery(pageNumber), cancellationToken);

    [HttpGet("AvailableToBeChild")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsAvailableToBeChildAsync([FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryIdNameDtoAvailableToBeChildCategoryQuery(null, parentId, childIds), cancellationToken);

    [HttpGet("AvailableToBeChild/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsAvailableToBeChildAsync([FromRoute] Guid id, [FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryIdNameDtoAvailableToBeChildCategoryQuery(id, parentId, childIds), cancellationToken);

    [HttpGet("AvailableToBeParent")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsAvailableToBeParentAsync([FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryIdNameDtoAvailableToBeParentCategoryQuery(null, childIds), cancellationToken);

    [HttpGet("AvailableToBeParent/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsAvailableToBeParentAsync([FromRoute] Guid id, [FromQuery] IEnumerable<Guid> childIds, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryIdNameDtoAvailableToBeParentCategoryQuery(id, childIds), cancellationToken);

    [HttpGet("CategoryParentId")]
    [ProducesResponseType(typeof(IEnumerable<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByCategoryParentIdAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryListDtoByCategoryParentQuery(), cancellationToken);

    [HttpGet("CategoryParentId/{categoryParentId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryListDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByCategoryParentIdAsync([FromRoute] Guid categoryParentId, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryListDtoByCategoryParentQuery(categoryParentId), cancellationToken);

    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<IdNameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsIdNameAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetsCategoryIdNameDtoQuery(), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(dto, new UpdateCategoryFormDtoCommand(id, dto), cancellationToken);
}