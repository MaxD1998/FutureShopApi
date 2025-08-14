using File.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace File.Infrastructure;

public static class Register
{
    public static void RegisterFileInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<FileContext>();

        services.AddScoped<IFileRepository, FileRepository>();
    }
}