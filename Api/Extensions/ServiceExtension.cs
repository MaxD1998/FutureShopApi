using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure.Settings;
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
}