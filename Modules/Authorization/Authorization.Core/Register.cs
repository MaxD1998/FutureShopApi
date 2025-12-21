using Authorization.Core.Interfaces.Services;
using Authorization.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Core;

public static class Register
{
    public static void RegisterAuthCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPermissionGroupService, PermissionGroupService>();
    }
}