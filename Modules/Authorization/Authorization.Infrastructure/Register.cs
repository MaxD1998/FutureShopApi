using Microsoft.Extensions.DependencyInjection;
using Authorization.Infrastructure.Repositories;
using Authorization.Core.Interfaces.Repositories;

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