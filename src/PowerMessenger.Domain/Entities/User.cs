
using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class User:BaseEntity<long>
{
    public long UserId { get; set; }
    public string? UserName { get; set; } 
    public string? Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Theme { get; set; }
    
    public ICollection<ChatParticipant>? ChatParticipants { get; set; }
    public ICollection<MessageStatus>? MessageStatuses { get; set; }
    public ICollection<Message>? Messages { get; set; }
    public ICollection<Message>? DeletedMessages { get; set; }
}
