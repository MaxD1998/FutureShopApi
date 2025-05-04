using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IAdCampaignRepository : IBaseRepository<AdCampaignEntity>
{
    Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken);
}

public class AdCampaignRepository(ShopContext context) : BaseRepository<ShopContext, AdCampaignEntity>(context), IAdCampaignRepository
{
    public Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        return _context.Set<AdCampaignEntity>()
            .Where(x => x.IsActive && x.Start <= today && today <= x.End)
            .Select(map)
            .ToListAsync(cancellationToken);
    }
}