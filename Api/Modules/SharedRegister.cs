using Shared.Core.Services;

namespace Api.Modules;

public static class SharedRegister
{
    public static void RegisterSharedModule(this IServiceCollection services)
    {
        services.RegisterServices();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IHeaderService, HeaderService>();
    }
}