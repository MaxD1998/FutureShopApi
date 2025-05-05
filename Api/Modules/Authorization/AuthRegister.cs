using Authorization.Core.Services;
using Authorization.Inrfrastructure;
using Authorization.Inrfrastructure.Repositories;
using Shared.Api.Middlewares;

namespace Api.Modules.Authorization;

public static class AuthRegister
{
    public static void RegisterAuthModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterRepositories();
        services.RegisterServices();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<AuthContext>();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<AuthContext>>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
    }
}