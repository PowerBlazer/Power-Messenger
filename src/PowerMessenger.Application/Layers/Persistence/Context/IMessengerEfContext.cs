using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Application.Layers.Persistence.Context;

public interface IMessengerEfContext
{
    DbSet<User> Users { get; }
    
    DbSet<Chat> Chats { get; }
    DbSet<ChatParticipant> ChatParticipants { get; }
    DbSet<ChatType> ChatTypes { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageStatus> MessageStatuses { get; }
    DbSet<MessageType> MessageTypes { get; }
    
    Task<int> SaveChangesAsync();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity<long>;
    void Attach<TEntity>(TEntity entity) where TEntity : BaseEntity<long>;
    public DatabaseFacade Database { get; }
}