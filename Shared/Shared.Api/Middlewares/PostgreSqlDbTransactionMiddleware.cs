using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;

namespace Shared.Api.Middlewares;

public class PostgreSqlDbTransactionMiddleware<TContext> : IMiddleware where TContext : BasePostgreSqlContext
{
    private readonly TContext _context;

    public PostgreSqlDbTransactionMiddleware(TContext context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await next.Invoke(context);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}