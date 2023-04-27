using Microsoft.EntityFrameworkCore;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Context;

public interface IEfContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Chat>? Chats { get; set; }
    public DbSet<ChatParticipant>? ChatParticipants { get; set; }
    public DbSet<ChatType>? ChatTypes { get; set; }
    public DbSet<Message>? Messages { get; set; }
    public DbSet<MessageStatus>? MessageStatuses { get; set; }
    public DbSet<MessageType>? MessageTypes { get; set; }
    
    Task<int> SaveChangesAsync();
}