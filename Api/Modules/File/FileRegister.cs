using Api.Extensions;
using File.Core;
using File.Core.EventHandlers;
using File.Core.Jobs;
using File.Infrastructure;
using Quartz;
using Shared.Api.Extensions;
using Shared.Api.Middlewares;

namespace Api.Modules.File;

public static class FileRegister
{
    public static void RegisterFileModule(this IServiceCollection services)
    {
        services.RegisterFileInfrastructure();
        services.RegisterFileCore();
        services.RegisterMiddlewares();

        services.AddRabbitReceiver(x =>
        {
            x.AddEventHandler<FilesToDeleteEventHandler>();
        });

        services.AddQuartz(cfg =>
        {
            cfg.AddJobAndTrigger<FileToDeleteJob>();
        });
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<MongoDbTransactionMiddleware<FileContext>>();
    }
}