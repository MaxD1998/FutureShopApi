using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaign.Queries;
public record GetAdCampaignFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<AdCampaignFormDto>>;

internal class GetAdCampaignFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetAdCampaignFormDtoByIdQuery, ResultDto<AdCampaignFormDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetAdCampaignFormDtoByIdQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<AdCampaignFormDto>> Handle(GetAdCampaignFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<AdCampaignEntity>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(AdCampaignFormDto.Map())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}