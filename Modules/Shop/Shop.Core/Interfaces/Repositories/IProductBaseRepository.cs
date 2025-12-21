using Shared.Core.Interfaces;
using Shop.Domain.Entities.ProductBases;

namespace Shop.Core.Interfaces.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>, IUpdateRepository<ProductBaseEntity>
{
    Task CreateOrUpdateAsync(ProductBaseEntity entity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}