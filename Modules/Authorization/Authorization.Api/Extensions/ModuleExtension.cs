using Authorization.Core;
using Authorization.Core.Interfaces.Services;
using Authorization.Core.Services;
using Authorization.Inrfrastructure;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Factories.FluentValidator;

namespace Authorization.Api.Extensions;

public static class ModuleExtension
{
    public static void RegisterAuthModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<AuthContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
        services.AddAutoMapper(typeof(CoreAssembly).Assembly);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFluentValidatorFactory, FluentValidatorFactory>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
    }
}