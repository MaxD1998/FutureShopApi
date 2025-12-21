using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Persistence.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Persistence.Repositories;

public interface IAdCampaignRepository : IBaseRepository<AdCampaignEntity>, IUpdateRepository<AdCampaignEntity>
{
    Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken);

    Task<TResult> GetActualByIdAsync<TResult>(Guid id, Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken);
}

internal class AdCampaignRepository(ShopContext context) : BaseRepository<ShopContext, AdCampaignEntity>(context), IAdCampaignRepository
{
    public Task<List<TResult>> GetActualAsync<TResult>(Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        return _context.Set<AdCampaignEntity>()
            .Where(x => x.IsActive && x.Start <= today && today <= x.End)
            .Select(map)
            .ToListAsync(cancellationToken);
    }

    public Task<TResult> GetActualByIdAsync<TResult>(Guid id, Expression<Func<AdCampaignEntity, TResult>> map, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        return _context.Set<AdCampaignEntity>()
            .Where(x => x.Id == id && x.IsActive && x.Start <= today && today <= x.End)
            .Select(map)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<AdCampaignEntity> UpdateAsync(Guid id, AdCampaignEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<AdCampaignEntity>()
            .Include(x => x.AdCampaignItems)
            .Include(x => x.AdCampaignProducts)
            .Include(x => x.Promotion)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}