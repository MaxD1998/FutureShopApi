using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Dtos;

namespace Shared.Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            if (exception is BaseException handleException)
            {
                context.Response.StatusCode = handleException.StatusCode;
                await context.Response.WriteAsJsonAsync(handleException.Error);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorDto()
                {
                    ErrorMessage = exception.Message
                });
            }
        }
    }
}