using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class MessageType : BaseEntity<long>
{
    public string? Type { get; set; } 
    public ICollection<Message>? Messages { get; set; }
}