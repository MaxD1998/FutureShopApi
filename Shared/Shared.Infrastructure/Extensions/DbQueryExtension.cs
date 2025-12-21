using Microsoft.EntityFrameworkCore;
using Shared.Shared.Dtos;
using Shared.Shared.Interfaces;

namespace Shared.Infrastructure.Extensions;

public static class DbQueryExtension
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> query, IFilter<T> filter, string lang) where T : class
        => filter.FilterExecute(query, lang);

    public static async Task<PageDto<T>> ToPageAsync<T>(this IQueryable<T> query, PaginationDto pagination, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pagination.PageNumber);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pagination.PageSize);

        var items = await query
            .Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        var recordsCount = await query.CountAsync(cancellationToken);
        var totalPages = recordsCount / pagination.PageSize + 1;

        return new PageDto<T>(pagination.PageNumber, items, totalPages);
    }
}