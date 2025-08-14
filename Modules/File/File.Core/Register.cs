using File.Core.JobServices;
using File.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace File.Core;

public static class Register
{
    public static void RegisterFileCore(this IServiceCollection services)
    {
        services.AddScoped<IFileJobService, FileJobService>();
        services.AddScoped<IFileService, FileService>();
    }
}