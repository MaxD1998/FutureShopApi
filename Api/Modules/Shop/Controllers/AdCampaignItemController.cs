using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Factories.FluentValidator;
using Shop.Core.Cqrs.AdCampaignItem.Commands;
using Shop.Core.Cqrs.AdCampaignItem.Queries;
using Shop.Core.Dtos.AdCampaignItem;

namespace Api.Modules.Shop.Controllers;

public class AdCampaignItemController : ShopModuleBaseController
{
    public AdCampaignItemController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateListAsync([FromForm] IFormFileCollection files, CancellationToken cancellationToken = default)
        => await ApiResponseAsync(new CreateListAdCampaignItemCommand(files), cancellationToken);

    /// <summary>
    /// It returns a file by his unique id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        => await ApiFileResponseAsync(new GetAdCampaignDocumentByIdQuery(id), cancellationToken);

    [HttpGet("Info/")]
    [ProducesResponseType(typeof(IEnumerable<AdCampaignItemInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListInfoByIdsAsync([FromQuery] IEnumerable<string> ids, CancellationToken cancellationToken)
        => await ApiResponseAsync(new GetListAdCampaignInfoDtoByIdsQuery(ids), cancellationToken);
}