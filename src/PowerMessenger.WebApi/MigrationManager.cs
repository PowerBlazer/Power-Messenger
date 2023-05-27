using Microsoft.EntityFrameworkCore;
using PowerMessenger.Infrastructure.Identity.Contexts;
using PowerMessenger.Infrastructure.Persistence.Context;

namespace PowerMessenger.WebApi;

public static class MigrationManager
{
    public static void MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        for (var i = 1; i <= 10; i++)
        {
            try
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<MessengerEfContext>();
                using var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();

                appContext.Database.Migrate();
                identityContext.Database.Migrate();

                break;
            }
            catch   
            {
                if (i == 10)
                {
                    throw;
                }

                Thread.Sleep(1000 * i);
            }
        }
    }
}