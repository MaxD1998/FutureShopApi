using Microsoft.EntityFrameworkCore;
using Shop.Domain.Aggregates.ProductBases.Entities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IProductParameterRepository
{
    Task<List<TResult>> GetListByProductIdAsync<TResult>(Guid productId, Expression<Func<ProductParameterEntity, TResult>> map, CancellationToken cancellationToken);
}

public class ProductParameterRepository(ShopContext context) : IProductParameterRepository
{
    private readonly ShopContext _context = context;

    public Task<List<TResult>> GetListByProductIdAsync<TResult>(Guid productId, Expression<Func<ProductParameterEntity, TResult>> map, CancellationToken cancellationToken)
        => _context.Set<ProductParameterEntity>().AsNoTracking().Where(x => x.ProductBase.Products.Any(x => x.Id == productId)).Select(map).ToListAsync(cancellationToken);
}