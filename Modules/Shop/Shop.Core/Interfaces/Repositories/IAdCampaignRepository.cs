using Shared.Core.Interfaces;
using Shop.Domain.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Interfaces.Repositories;

public interface IAdCampaignRepository : IBaseRepository<AdCampaignEntity>, IUpdateRepository<AdCampaignEntity>
{
    Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken);

    Task<TResult> GetActualByIdAsync<TResult>(Guid id, Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken);
}