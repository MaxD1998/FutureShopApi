using File.Infrastructure;
using Shared.Api.Middlewares;

namespace Api.Modules.File;

public static class FileRegister
{
    public static void RegisterFileModule(this IServiceCollection services)
    {
        services.AddScoped<FileContext>();
        services.AddScoped<MongoDbTransactionMiddleware<FileContext>>();
    }
}