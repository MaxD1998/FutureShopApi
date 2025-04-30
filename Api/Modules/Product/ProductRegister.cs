using Api.Extensions;
using FluentValidation;
using Product.Core;
using Product.Core.Jobs;
using Product.Core.Services;
using Product.Infrastructure;
using Product.Infrastructure.Repositories;
using Quartz;
using Shared.Api.Middlewares;

namespace Api.Modules.Product;

public static class ProductRegister
{
    public static void RegisterProductModule(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.RegisterRepositories();
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

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductBaseRepository, ProductBaseRepository>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductBaseService, ProductBaseService>();
    }
}