using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;

namespace Shared.Api.Middlewares;
public class DbTransactionMiddleware<TContext> : IMiddleware where TContext : BaseContext
{
    private readonly TContext _context;

    public DbTransactionMiddleware(TContext context)
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
