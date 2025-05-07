using File.Core.EventHandlers;
using File.Core.Services;
using File.Infrastructure;
using File.Infrastructure.Repositories;
using Shared.Api.Extensions;
using Shared.Api.Middlewares;

namespace Api.Modules.File;

public static class FileRegister
{
    public static void RegisterFileModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterRepositories();
        services.RegisterServices();
        services.RegisterMiddlewares();

        services.AddRabbitReceiver(x =>
        {
            x.AddEventHandler<FilesToDeleteEventHandler>();
        });
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<FileContext>();
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<MongoDbTransactionMiddleware<FileContext>>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
    }
}