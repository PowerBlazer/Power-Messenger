using Microsoft.EntityFrameworkCore;
using PowerMessenger.Infrastructure.Identity.Configuration;
using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Contexts;

public class IdentityContext : DbContext
{
    public DbSet<IdentityUser> IdentityUsers => Set<IdentityUser>();
    public DbSet<IdentityToken> IdentityTokens => Set<IdentityToken>();

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityTokenConfiguration());
    }
}