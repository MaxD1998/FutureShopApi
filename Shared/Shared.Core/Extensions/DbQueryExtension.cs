using Microsoft.EntityFrameworkCore;
using Shared.Core.Dtos;

namespace Shared.Core.Extensions;

public static class DbQueryExtension
{
    public static async Task<PageDto<T>> ToPageAsync<T>(this IQueryable<T> query, int pageNumber, CancellationToken cancellationToken = default) where T : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);

        var itemsCount = 50;
        var items = await query
            .Skip(itemsCount * (pageNumber - 1))
            .Take(itemsCount)
            .ToListAsync(cancellationToken);

        var recordsCount = await query.CountAsync(cancellationToken);
        var totalPages = (recordsCount / itemsCount) + 1;

        return new PageDto<T>(pageNumber, items, totalPages);
    }
}