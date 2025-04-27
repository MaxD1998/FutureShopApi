using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaign.Queries;
public record GetActualAdCampaignDtoQuery : IRequest<ResultDto<AdCampaignDto>>;

internal class GetActualAdCampaignDtoQueryHandler : BaseService, IRequestHandler<GetActualAdCampaignDtoQuery, ResultDto<AdCampaignDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetActualAdCampaignDtoQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<AdCampaignDto>> Handle(GetActualAdCampaignDtoQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        var results = await _context.Set<AdCampaignEntity>()
            .AsNoTracking()
            .Where(x => x.IsActive && x.Start <= today && today <= x.End)
            .Select(AdCampaignDto.Map())
            .ToListAsync(cancellationToken);

        return Success(AdCampaignDto.Merge(results));
    }
}