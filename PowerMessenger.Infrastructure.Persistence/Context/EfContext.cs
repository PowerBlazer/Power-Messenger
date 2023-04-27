using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Context;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Infrastructure.Persistence.Configuration;

namespace PowerMessenger.Infrastructure.Persistence.Context;

public sealed class EfContext : DbContext,IEfContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Chat>? Chats { get; set; }
    public DbSet<ChatParticipant>? ChatParticipants { get; set; }
    public DbSet<ChatType>? ChatTypes { get; set; }
    public DbSet<Message>? Messages { get; set; }
    public DbSet<MessageStatus>? MessageStatuses { get; set; }
    public DbSet<MessageType>? MessageTypes { get; set; }
    
    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new ChatParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new ChatTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new MessageStatusConfiguration());
        modelBuilder.ApplyConfiguration(new MessageTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}