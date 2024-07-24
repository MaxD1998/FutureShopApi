using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;

namespace Shared.Api.Middlewares;

public class MongoDbTransactionMiddleware<TContext> : IMiddleware where TContext : BaseMongoDbContext
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
                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}