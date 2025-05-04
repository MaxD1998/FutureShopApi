using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public interface IPurchaseListRepository : IBaseRepository<PurchaseListEntity>
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

        var result = await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}