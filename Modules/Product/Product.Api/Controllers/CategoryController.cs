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
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryInputDto dto)
        => await ApiResponseAsync(dto, new CreateCategoryDtoCommand(dto));

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsAsync()
        => await ApiResponseAsync(new GetsCategoryDtoQuery());

    [HttpGet("CategoryParentId")]
    [HttpGet("CategoryParentId/{categoryParentId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetsByCategoryParentIdAsync([FromRoute] Guid? categoryParentId = null)
        => await ApiResponseAsync(new GetsCategoryDtoByCategoryParentQuery(categoryParentId));
}