using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Dtos;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.AdCampaign.Commands;
using Shop.Core.Cqrs.AdCampaign.Queries;
using Shop.Core.Dtos.AdCampaign;

namespace Api.Modules.Shop.Controllers;

public class AdCampaignController : ShopModuleBaseController
{
    public AdCampaignController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] AdCampaignFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateAdCampaignFormDtoCommand(dto), cancellationToken);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new DeleteAdCampaignByIdCommand(id), cancellationToken);

    [HttpGet("Actual")]
    [ProducesResponseType(typeof(AdCampaignDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetActualAsync(CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetActualAdCampaignDtoQuery(), cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetAdCampaignFormDtoByIdQuery(id), cancellationToken);

    [HttpGet("Page/{pageNumber:int}")]
    [ProducesResponseType(typeof(PageDto<AdCampaignListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromRoute] int pageNumber, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new GetPageAdCampaignListDtoQuery(pageNumber), cancellationToken);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AdCampaignFormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] AdCampaignFormDto dto, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new UpdateAdCampaignFormDtoCommand(id, dto), cancellationToken);
}