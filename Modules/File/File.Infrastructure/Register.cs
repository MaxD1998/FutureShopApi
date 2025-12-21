using Microsoft.Extensions.DependencyInjection;
using File.Infrastructure.Repositories;
using File.Core.Interfaces.Repositories;

namespace File.Infrastructure;

public static class Register
{
    public static void RegisterFileInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<FileContext>();

        services.AddScoped<IFileRepository, FileRepository>();
    }
}