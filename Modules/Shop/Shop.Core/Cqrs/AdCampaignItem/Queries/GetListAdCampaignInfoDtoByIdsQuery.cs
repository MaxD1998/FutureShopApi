using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.AdCampaignItem;
using Shop.Domain.Documents;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaignItem.Queries;

public record GetListAdCampaignInfoDtoByIdsQuery(IEnumerable<string> Ids) : IRequest<ResultDto<IEnumerable<AdCampaignItemInfoDto>>>;

internal class GetListAdCampaignInfoDtoByIdsQueryHandler : BaseService, IRequestHandler<GetListAdCampaignInfoDtoByIdsQuery, ResultDto<IEnumerable<AdCampaignItemInfoDto>>>
{
    private readonly ShopMongoDbContext _contextMongoDb;
    private readonly ShopPostgreSqlContext _contextPostgreSql;

    public GetListAdCampaignInfoDtoByIdsQueryHandler(ShopMongoDbContext contextMongoDb, ShopPostgreSqlContext contextPostgreSql)
    {
        _contextMongoDb = contextMongoDb;
        _contextPostgreSql = contextPostgreSql;
    }

    public async Task<ResultDto<IEnumerable<AdCampaignItemInfoDto>>> Handle(GetListAdCampaignInfoDtoByIdsQuery request, CancellationToken cancellationToken)
    {
        var documentsTask = _contextMongoDb
            .Set<AdCampaignItemDocument>()
            .Find(x => request.Ids.Contains(x.Id))
            .Project(x => new { x.Id, x.ContentType, x.Length, x.Name })
            .ToListAsync(cancellationToken);

        var adCampaignItemsTask = _contextPostgreSql
            .Set<AdCampaignItemEntity>()
            .Where(x => request.Ids.Contains(x.FileId))
            .Select(x => new { x.FileId, x.Lang })
            .ToListAsync(cancellationToken);

        await Task.WhenAll(documentsTask, adCampaignItemsTask);

        var documents = await documentsTask;
        var adCampaignItems = await adCampaignItemsTask;
        var results = documents.Select(x => new AdCampaignItemInfoDto(x.Id, x.ContentType, x.Length, x.Name)).ToList();

        results.ForEach(result =>
        {
            var adCampaignItem = adCampaignItems.FirstOrDefault(x => x.FileId == result.Id);

            if (adCampaignItem != null)
                result.Lang = adCampaignItem.Lang;
        });

        return Success(results.AsEnumerable());
    }
}