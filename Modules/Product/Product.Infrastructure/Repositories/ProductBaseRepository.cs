using Microsoft.EntityFrameworkCore;
using Product.Domain.Aggregates.ProductBases;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseAggregate>, IUpdateRepository<ProductBaseAggregate>
{
}

public class ProductBaseRepository(ProductContext context) : BaseRepository<ProductContext, ProductBaseAggregate>(context), IProductBaseRepository
{
    public async Task<ProductBaseAggregate> UpdateAsync(Guid id, ProductBaseAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductBaseAggregate>()
            .Include(x => x.Products)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}