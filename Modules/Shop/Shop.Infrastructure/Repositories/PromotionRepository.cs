using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Promotions;

namespace Shop.Infrastructure.Repositories;

public interface IPromotionRepository : IBaseRepository<PromotionEntity>, IUpdateRepository<PromotionEntity>
{
    Task<List<PromotionEntity>> GetActivePromotionsAsync(List<string> codes, CancellationToken cancellationToken);
}

public class PromotionRepository(ShopContext context) : BaseRepository<ShopContext, PromotionEntity>(context), IPromotionRepository
{
    public async Task<List<PromotionEntity>> GetActivePromotionsAsync(List<string> codes, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        var results = await _context.Set<PromotionEntity>()
            .Include(x => x.PromotionProducts)
            .Where(x => x.Start <= today && today < x.End && codes.Contains(x.Code))
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<PromotionEntity> UpdateAsync(Guid id, PromotionEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<PromotionEntity>()
            .Include(x => x.PromotionProducts)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);
        entityToUpdate.Validate();

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}