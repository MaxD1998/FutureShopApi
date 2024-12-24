using Api.Extensions;
using Api.Modules;
using Api.Modules.Authorization;
using Api.Modules.Product;
using Api.Modules.Shop;
using Api.Modules.Warehouse;
using Authorization.Inrfrastructure;
using Product.Infrastructure;
using Quartz.AspNetCore;
using Shared.Api.Middlewares;
using Shared.Infrastructure;
using Shop.Infrastructure;
using System.Reflection;
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
        services.AddSingleton<RabbitMqContext>();

        services.RegisterSharedModule();
        services.RegisterAuthModule();
        services.RegisterProductModule();
        services.RegisterShopModule();
        services.RegisterWarehouseModule();

        services.AddControllers();
        services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
            config.CustomSchemaIds(type => type.ToString());
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DefaultModelsExpandDepth(-1);
            });
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<AuthContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<ProductPostgreSqlContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<ShopContext>>();
        app.UseMiddleware<PostgreSqlDbTransactionMiddleware<WarehouseContext>>();
        app.UseMiddleware<MongoDbTransactionMiddleware<ProductMongoDbContext>>();

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