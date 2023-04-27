using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerMessenger.Infrastructure.Persistence.Context;

namespace PowerMessenger.Infrastructure.Persistence;

public static class MigrationManager
{
    public static void MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<EfContext>();

        appContext.Database.Migrate();
    }
}