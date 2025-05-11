using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Interfaces;

namespace Shared.Infrastructure.Extensions;

public static class DbQueryExtension
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> query, IFilter<T> filter, string lang) where T : class
        => filter.FilterExecute(query, lang);

    public static async Task<PageDto<T>> ToPageAsync<T>(this IQueryable<T> query, int pageNumber, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);

        var itemsCount = 25;
        var items = await query
            .Skip(itemsCount * (pageNumber - 1))
            .Take(itemsCount)
            .ToListAsync(cancellationToken);

        var recordsCount = await query.CountAsync(cancellationToken);
        var totalPages = recordsCount / itemsCount + 1;

        return new PageDto<T>(pageNumber, items, totalPages);
    }
}