using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Services;

namespace Shared.Core;

public static class Register
{
    public static void RegisterSharedCore(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IHeaderService, HeaderService>();
    }
}