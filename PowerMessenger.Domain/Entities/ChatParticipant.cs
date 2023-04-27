using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class ChatParticipant : BaseEntity<long>
{
    public long ChatId { get; set; }
    public long UserId { get; set; }
    public string? Role { get; set; }
    
    public Chat? Chat { get; set; }
    public User? User { get; set; }
}