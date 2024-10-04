using Api.Extensions;
using FluentValidation;
using Product.Core;
using Product.Core.EventHandlers;
using Product.Core.Interfaces.Services;
using Product.Core.Jobs;
using Product.Core.Services;
using Product.Infrastructure;
using Quartz;
using Shared.Api.Extensions;
using Shared.Api.Middlewares;

namespace Api.Modules.Product;

public static class ProductRegister
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterServices();
        services.RegisterQuartzJobs();
        services.RegisterMiddlewares();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<ProductPostgreSqlContext>();
        services.AddScoped<ProductMongoDbContext>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CoreAssembly).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(CoreAssembly).Assembly);
        services.AddRabbitReceiver(x =>
        {
            x.AddEventHandler<UserEventHandler>();
        });
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlDbTransactionMiddleware<ProductPostgreSqlContext>>();
        services.AddScoped<MongoDbTransactionMiddleware<ProductMongoDbContext>>();
    }

    private static void RegisterQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.AddJobAndTrigger<DeleteNotAssignedPhotoJob>();
        });
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IHeaderService, HeaderService>();
    }
}