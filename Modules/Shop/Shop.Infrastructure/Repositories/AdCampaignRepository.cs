using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IAdCampaignRepository : IBaseRepository<AdCampaignAggregate>, IUpdateRepository<AdCampaignAggregate>
{
    Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignAggregate, TResult>> map, CancellationToken cancellationToken);
}

public class AdCampaignRepository(ShopContext context) : BaseRepository<ShopContext, AdCampaignAggregate>(context), IAdCampaignRepository
{
    public Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignAggregate, TResult>> map, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        return _context.Set<AdCampaignAggregate>()
            .Where(x => x.IsActive && x.Start <= today && today <= x.End)
            .Select(map)
            .ToListAsync(cancellationToken);
    }

    public async Task<AdCampaignAggregate> UpdateAsync(Guid id, AdCampaignAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<AdCampaignAggregate>()
            .Include(x => x.AdCampaignItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}