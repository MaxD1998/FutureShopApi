using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Cqrs.Category.Commands;
using Product.Core.Cqrs.Category.Queries;
using Product.Core.Dtos.Category;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Product.Api.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryFormDto dto)
        => await ApiResponseAsync(dto, new CreateCategoryFormDtoCommand(dto));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        => await ApiResponseAsync(new DeleteCategoryByIdCommand(id));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        => await ApiResponseAsync(new GetCategoryFormDtoByIdQuery(id));

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAsync()
        => await ApiResponseAsync(new GetsCategoryDtoQuery());

    [HttpGet("AvailableToBeChild")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAvailableToBeChildAsync([FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds)
        => await ApiResponseAsync(new GetsCategoryDtoAvailableToBeChildCategoryQuery(null, parentId, childIds));

    [HttpGet("AvailableToBeChild/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAvailableToBeChildAsync([FromRoute] Guid id, [FromQuery] Guid? parentId, [FromQuery] IEnumerable<Guid> childIds)
        => await ApiResponseAsync(new GetsCategoryDtoAvailableToBeChildCategoryQuery(id, parentId, childIds));

    [HttpGet("AvailableToBeParent")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAvailableToBeParentAsync([FromQuery] IEnumerable<Guid> childIds)
        => await ApiResponseAsync(new GetsCategoryDtoAvailableToBeParentCategoryQuery(null, childIds));

    [HttpGet("AvailableToBeParent/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAvailableToBeParentAsync([FromRoute] Guid id, [FromQuery] IEnumerable<Guid> childIds)
        => await ApiResponseAsync(new GetsCategoryDtoAvailableToBeParentCategoryQuery(id, childIds));

    [HttpGet("CategoryParentId")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByCategoryParentIdAsync()
        => await ApiResponseAsync(new GetsCategoryDtoByCategoryParentQuery());

    [HttpGet("CategoryParentId/{categoryParentId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByCategoryParentIdAsync([FromRoute] Guid categoryParentId)
        => await ApiResponseAsync(new GetsCategoryDtoByCategoryParentQuery(categoryParentId));

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryFormDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CategoryFormDto dto)
        => await ApiResponseAsync(dto, new UpdateCategoryFormDtoCommand(id, dto));
}