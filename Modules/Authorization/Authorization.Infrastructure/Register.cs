using Authorization.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Infrastructure;

public static class Register
{
    public static void RegisterAuthInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AuthContext>();

        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}