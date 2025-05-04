using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shop.Domain.Entities;
using Shop.Infrastructure.Models.Product;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IProductRepository : IBaseRepository<ProductEntity>
{
    Task CreateOrUpdateForEventAsync(ProductEntity eventEntity, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<TResult>> GetListByCategoryIdAsync<TResult>(GetProductListByCategoryIdParams parameters, Expression<Func<ProductEntity, TResult>> map, CancellationToken cancellationToken);
}

public class ProductRepository(ShopContext context) : BaseRepository<ShopContext, ProductEntity>(context), IProductRepository
{
    public async Task CreateOrUpdateForEventAsync(ProductEntity eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.Id, cancellationToken);

        if (entity is null)
            await _context.Set<ProductEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductEntity>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<TResult>> GetListByCategoryIdAsync<TResult>(GetProductListByCategoryIdParams parameters, Expression<Func<ProductEntity, TResult>> map, CancellationToken cancellationToken)
    {
        var categoryIds = await GetCategoryIds([parameters.CategoryId], cancellationToken);
        var results = await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Where(x => categoryIds.Contains(x.ProductBase.CategoryId) && x.IsActive)
            .Filter(parameters.Filter, parameters.Lang)
            .Select(map)
            .ToListAsync(cancellationToken);

        return results;
    }

    private async Task<IEnumerable<Guid>> GetCategoryIds(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var results = new List<Guid>();
        var categories = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Where(x => categoryIds.Contains(x.Id))
            .ToListAsync();

        if (categories.Count > 0)
        {
            results.AddRange(categories.Select(x => x.Id));

            var subCategories = categories.SelectMany(x => x.SubCategories).ToList();

            if (subCategories.Count > 0)
                results.AddRange(await GetCategoryIds(subCategories.Select(x => x.Id), cancellationToken));
        }

        return results;
    }
}