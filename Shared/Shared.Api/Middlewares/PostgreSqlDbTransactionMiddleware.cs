using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;
using System.Net;

namespace Shared.Api.Middlewares;

public class PostgreSqlDbTransactionMiddleware<TContext> : IMiddleware where TContext : BaseContext
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

            if (context.Response.StatusCode is (int)HttpStatusCode.OK or (int)HttpStatusCode.NoContent)
                await transaction.CommitAsync();
            else
                await transaction.RollbackAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}