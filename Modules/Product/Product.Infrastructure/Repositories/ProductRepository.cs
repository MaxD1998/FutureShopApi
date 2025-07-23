using Microsoft.EntityFrameworkCore;
using Product.Domain.Aggregates.Products;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;

namespace Product.Infrastructure.Repositories;

public interface IProductRepository : IBaseRepository<ProductAggregate>, IUpdateRepository<ProductAggregate>
{
}

public class ProductRepository(ProductContext context) : BaseRepository<ProductContext, ProductAggregate>(context), IProductRepository
{
    public async Task<ProductAggregate> UpdateAsync(Guid id, ProductAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductAggregate>()
            .Include(x => x.ProductPhotos)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}