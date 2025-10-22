using Shared.Core;

namespace Api.Modules;

public static class SharedRegister
{
    public static void RegisterSharedModule(this IServiceCollection services)
    {
        services.RegisterSharedCore();
    }
}