using Microsoft.EntityFrameworkCore;
using Product.Domain.Aggregates.Categories;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Product.Infrastructure.Repositories;

public interface ICategoryRepository : IBaseRepository<CategoryAggregate>, IUpdateRepository<CategoryAggregate>
{
    Task<List<CategoryAggregate>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken);

    Task<List<TResult>> GetListPotentialParentCategories<TResult>(Guid? id, List<Guid> childIds, Expression<Func<CategoryAggregate, TResult>> map, CancellationToken cancellationToken);

    Task<List<TResult>> GetListPotentialSubcategoriesAsync<TResult>(Guid? id, Guid? parentId, List<Guid> childIds, Expression<Func<CategoryAggregate, TResult>> map, CancellationToken cancellationToken);
}

public class CategoryRepository(ProductContext context) : BaseRepository<ProductContext, CategoryAggregate>(context), ICategoryRepository
{
    public Task<List<CategoryAggregate>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken)
        => _context.Set<CategoryAggregate>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    public async Task<List<TResult>> GetListPotentialParentCategories<TResult>(Guid? id, List<Guid> childIds, Expression<Func<CategoryAggregate, TResult>> map, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryAggregate>()
            .AsNoTracking()
        .AsQueryable();

        if (id.HasValue)
            query = query.Where(x => x.Id != id.Value);

        if (childIds.Any())
        {
            var resultChildIds = childIds.Concat(await ExceptionIdsAsync(childIds, cancellationToken));
            query = query.Where(x => !childIds.Contains(x.Id));
        }

        var results = await query
            .Select(map)
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<List<TResult>> GetListPotentialSubcategoriesAsync<TResult>(Guid? id, Guid? parentId, List<Guid> childIds, Expression<Func<CategoryAggregate, TResult>> map, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryAggregate>()
        .AsNoTracking()
        .AsQueryable();

        query = id.HasValue
        ? query = query.Where(x => x.Id != id && (x.ParentCategoryId == id.Value || x.ParentCategoryId == null))
        : query = query.Where(x => x.ParentCategoryId == null);

        if (parentId.HasValue)
        {
            var parentIds = await GetExceptionIdAsync(parentId.Value, cancellationToken);
            query = query.Where(x => !parentIds.Contains(x.Id));
        }

        if (childIds.Any())
            query = query.Where(x => !childIds.Contains(x.Id));

        var results = await query
            .Select(map)
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task<CategoryAggregate> UpdateAsync(Guid id, CategoryAggregate entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<CategoryAggregate>()
            .Include(x => x.SubCategories)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entityToUpdate == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }

    private async Task<IEnumerable<Guid>> ExceptionIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var results = await _context.Set<CategoryAggregate>()
            .Where(x => ids.Contains(x.Id))
            .SelectMany(x => x.SubCategories.Select(x => x.Id))
            .ToListAsync(cancellationToken);

        if (results.Count == 0)
            return [];

        return results.Concat(await ExceptionIdsAsync(results, cancellationToken));
    }

    private async Task<IEnumerable<Guid>> GetExceptionIdAsync(Guid parentId, CancellationToken cancellationToken = default)
    {
        var results = new List<Guid>();
        var exceptionCategory = await _context.Set<CategoryAggregate>()
            .FirstOrDefaultAsync(x => x.Id == parentId, cancellationToken);

        if (exceptionCategory == null)
            return results;

        results.Add(exceptionCategory.Id);

        if (exceptionCategory.ParentCategoryId.HasValue)
            results.AddRange(await GetExceptionIdAsync((Guid)exceptionCategory.ParentCategoryId, cancellationToken));

        return results;
    }
}