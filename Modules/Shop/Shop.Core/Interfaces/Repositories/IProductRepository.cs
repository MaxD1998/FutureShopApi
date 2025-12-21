using Shared.Core.Interfaces;
using Shop.Core.Models.Products;
using Shop.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<ProductEntity>, IUpdateRepository<ProductEntity>
{
    Task CreateOrUpdateForEventAsync(ProductEntity eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<TResult>> GetListByCategoryIdAsync<TResult>(GetProductListByCategoryIdParams parameters, Expression<Func<ProductEntity, TResult>> map, CancellationToken cancellationToken);
}