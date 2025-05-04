using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public interface IPurchaseListRepository : IBaseRepository<PurchaseListEntity>, IUpdateRepository<PurchaseListEntity>
{
}

public class PurchaseListRepository(ShopContext context) : BaseRepository<ShopContext, PurchaseListEntity>(context), IPurchaseListRepository
{
    public override async Task<PurchaseListEntity> CreateAsync(PurchaseListEntity entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<PurchaseListEntity> UpdateAsync(Guid id, PurchaseListEntity entity, CancellationToken cancellationToken)
    {
        if (entity.UserId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.Id != id && x.UserId == entity.UserId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return null;
        }

        var entityToUpdate = await _context.Set<PurchaseListEntity>()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}