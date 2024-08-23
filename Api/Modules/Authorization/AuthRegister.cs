using Authorization.Core;
using Authorization.Core.Interfaces.Services;
using Authorization.Core.Services;
using Authorization.Inrfrastructure;
using FluentValidation;
using Shared.Api.Middlewares;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Authorization;

public static class AuthRegister
{
    public static void RegisterAuthModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<AuthContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<AuthContext>>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFluentValidatorFactory, FluentValidatorFactory>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
    }
}