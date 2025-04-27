using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.AdCampaign;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaign.Queries;
public record GetPageAdCampaignListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<AdCampaignListDto>>>;

internal class GetPageAdCampaignListDtoQueryHandler : BaseService, IRequestHandler<GetPageAdCampaignListDtoQuery, ResultDto<PageDto<AdCampaignListDto>>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetPageAdCampaignListDtoQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<PageDto<AdCampaignListDto>>> Handle(GetPageAdCampaignListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<AdCampaignEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(AdCampaignListDto.Map())
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}