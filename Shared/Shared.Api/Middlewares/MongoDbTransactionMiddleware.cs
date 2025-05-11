using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;
using System.Net;

namespace Shared.Api.Middlewares;

public class MongoDbTransactionMiddleware<TContext> : IMiddleware where TContext : BaseNoSqlContext
{
    private readonly TContext _context;

    public MongoDbTransactionMiddleware(TContext context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (var session = await _context.Client.StartSessionAsync())
        {
            session.StartTransaction();

            try
            {
                await next.Invoke(context);

                if (context.Response.StatusCode is (int)HttpStatusCode.OK or (int)HttpStatusCode.NoContent)
                    await session.CommitTransactionAsync();
                else
                    await session.AbortTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}