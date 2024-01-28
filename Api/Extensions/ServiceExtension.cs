using Core;
using Core.Interfaces.Services;
using Core.Services;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos.Settings;
using Shared.Factories.FluentValidation;
using Shared.Middlewares;
using System.Text;

namespace Api.Extensions;

public static class ServiceExtension
{
    public static void AddAppsettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ConnectionSettings>()
            .Bind(configuration.GetSection(nameof(ConnectionSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(nameof(JwtSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<RefreshTokenSettings>()
            .Bind(configuration.GetSection(nameof(RefreshTokenSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
        services.AddAutoMapper(typeof(CoreAssembly).Assembly);
        services.AddScoped<ErrorHandlingMiddleware>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFluentValidatorFactory, FluentValidatorFactory>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
    }
}