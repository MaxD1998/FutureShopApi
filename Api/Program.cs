using Api.Extensions;
using Api.Modules.Authorization;
using Api.Modules.Product;
using Authorization.Inrfrastructure;
using Product.Infrastructure;
using Shared.Api.Middlewares;

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

        services.AddScoped<ErrorHandlingMiddleware>();

        services.RegisterAuthModule();
        services.RegisterProductModule();

        services.AddControllers();
        services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<DbTransactionMiddleware<AuthContext>>();
        app.UseMiddleware<DbTransactionMiddleware<ProductContext>>();

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