using File.Core.JobServices;
using File.Core.Services;
using File.Core.Interfaces.Services;
using File.Core.JobServices.Interfaces;
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