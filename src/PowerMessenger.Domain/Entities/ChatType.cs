using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;


public class ChatType : BaseEntity<long>
{
    public string? Type { get; set; } 
    
    public ICollection<Chat>? Chats { get; set; }
}