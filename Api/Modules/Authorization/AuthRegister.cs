using Authorization.Core;
using Authorization.Infrastructure;
using Shared.Api.Middlewares;

namespace Api.Modules.Authorization;

public static class AuthRegister
{
    public static void RegisterAuthModule(this IServiceCollection services)
    {
        services.RegisterAuthInfrastructure();
        services.RegisterAuthCore();
        services.RegisterMiddlewares();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<AuthContext>>();
    }
}