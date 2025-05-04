using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IAdCampaignRepository : IBaseRepository<AdCampaignEntity>, IUpdateRepository<AdCampaignEntity>
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

    public async Task<AdCampaignEntity> UpdateAsync(Guid id, AdCampaignEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<AdCampaignEntity>()
            .Include(x => x.AdCampaignItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}