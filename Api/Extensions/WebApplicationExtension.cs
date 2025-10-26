using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;

namespace Api.Extensions;

public static class WebApplicationExtension
{
    public static void MigrateDatabase<TContext>(this WebApplication webApplication) where TContext : BaseContext
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            var pendingMigrations = context.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
                context.Database.Migrate();
        }
    }
}