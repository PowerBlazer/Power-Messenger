using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class Chat : BaseEntity<long>
{
    public string? Name { get; set; } 
    public DateTime DateCreate { get; set; }
    public string? Photo { get; set; }
    public long ChatTypeId { get; set; }
    public string? Description { get; set; }
   
    public ChatType? ChatType { get; set; }
    public ICollection<ChatParticipant>? ChatParticipants { get; set; }
    public ICollection<Message>? Messages { get; set; }
    public ICollection<MessageStatus>? MessageStatuses { get; set; }
}