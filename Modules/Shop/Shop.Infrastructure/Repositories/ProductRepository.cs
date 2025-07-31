using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Aggregates.Categories;
using Shop.Domain.Aggregates.Products;
using Shop.Infrastructure.Models.Product;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

public interface IProductRepository : IBaseRepository<ProductAggregate>, IUpdateRepository<ProductAggregate>
{
    Task CreateOrUpdateForEventAsync(ProductAggregate eventEntity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<List<TResult>> GetListByCategoryIdAsync<TResult>(GetProductListByCategoryIdParams parameters, Expression<Func<ProductAggregate, TResult>> map, CancellationToken cancellationToken);
}

public class ProductRepository(ShopContext context) : BaseRepository<ShopContext, ProductAggregate>(context), IProductRepository
{
    public async Task CreateOrUpdateForEventAsync(ProductAggregate eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductAggregate>()
            .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.ExternalId, cancellationToken);

        if (entity is null)
            await _context.Set<ProductAggregate>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductAggregate>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductAggregate>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<TResult>> GetListByCategoryIdAsync<TResult>(GetProductListByCategoryIdParams parameters, Expression<Func<ProductAggregate, TResult>> map, CancellationToken cancellationToken)
    {
        var categoryIds = await GetCategoryIds([parameters.CategoryId], cancellationToken);
        var results = await _context.Set<ProductAggregate>()
            .AsNoTracking()
            .Where(x => categoryIds.Contains(x.ProductBase.CategoryId) && x.IsActive)
            .Filter(parameters.Filter, parameters.Lang)
            .Select(map)
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<ProductAggregate> UpdateAsync(Guid id, ProductAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductAggregate>()
            .Include(x => x.Prices)
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }

    private async Task<IEnumerable<Guid>> GetCategoryIds(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var results = new List<Guid>();
        var categories = await _context.Set<CategoryAggregate>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Where(x => categoryIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

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