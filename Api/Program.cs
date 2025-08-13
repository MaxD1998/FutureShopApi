using Api.Extensions;
using Api.Modules;
using Api.Modules.Authorization;
using Api.Modules.File;
using Api.Modules.Product;
using Api.Modules.Shop;
using Api.Modules.Warehouse;
using Api.OpenApi;
using Authorization.Infrastructure;
using File.Infrastructure;
using Product.Infrastructure;
using Quartz.AspNetCore;
using Scalar.AspNetCore;
using Shared.Api.Middlewares;
using Shared.Infrastructure;
using Shop.Infrastructure;
using Warehouse.Infrastructure;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;

        // Add services to the container.

        services.AddAppsettings(config);
        services.AddJwtAuthentication(config);
        services.AddQuartzServer(config => config.WaitForJobsToComplete = true);

        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddSingleton<IRabbitMqContext, RabbitMqContext>();

        services.RegisterSharedModule();
        services.RegisterAuthModule();
        services.RegisterFileModule();
        services.RegisterProductModule();
        services.RegisterShopModule();
        services.RegisterWarehouseModule();

        services.AddControllers();
        services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(opt =>
            {
                opt.Theme = ScalarTheme.DeepSpace;
                opt.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                opt.ShowSidebar = true;
                opt.AddApiKeyAuthentication("Bearer", config =>
                {
                    config.Value = "my-token";
                });
            });
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<AuthContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<ProductContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<ShopContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<WarehouseContext>>();
        app.UseMiddleware<MongoDbTransactionMiddleware<FileContext>>();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200"));

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}