using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>
{
    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class ProductBaseRepository(ShopContext context) : BaseRepository<ShopContext, ProductBaseEntity>(context), IProductBaseRepository
{
    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseEntity>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);
}