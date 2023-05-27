using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Infrastructure.Persistence.Configuration;

namespace PowerMessenger.Infrastructure.Persistence.Context;

public class MessengerEfContext : DbContext,IMessengerEfContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
    public DbSet<ChatType> ChatTypes => Set<ChatType>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageStatus> MessageStatuses => Set<MessageStatus>();
    public DbSet<MessageType> MessageTypes => Set<MessageType>();
    
    public MessengerEfContext(DbContextOptions<MessengerEfContext> options) : base(options)
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