using MediatR;
using MongoDB.Driver;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Documents;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaignItem.Queries;
public record GetAdCampaignDocumentByIdQuery(string Id) : IRequest<ResultDto<AdCampaignItemDocument>>;

internal class GetAdCampaignDocumentByIdQueryHandler : BaseService, IRequestHandler<GetAdCampaignDocumentByIdQuery, ResultDto<AdCampaignItemDocument>>
{
    private readonly ShopMongoDbContext _context;

    public GetAdCampaignDocumentByIdQueryHandler(ShopMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<AdCampaignItemDocument>> Handle(GetAdCampaignDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<AdCampaignItemDocument>().Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        return Success(result);
    }
}