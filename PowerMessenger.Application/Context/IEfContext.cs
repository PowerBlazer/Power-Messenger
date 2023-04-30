using Microsoft.EntityFrameworkCore;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Context;

public interface IEfContext
{
    DbSet<User> Users { get; }
    DbSet<Chat> Chats { get; }
    DbSet<ChatParticipant> ChatParticipants { get; }
    DbSet<ChatType> ChatTypes { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageStatus> MessageStatuses { get; }
    DbSet<MessageType> MessageTypes { get; }
    
    Task<int> SaveChangesAsync();
}