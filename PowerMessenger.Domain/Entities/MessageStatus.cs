using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class MessageStatus : BaseEntity<long>
{
    public long UserId { get; set; }
    public long MessageId { get; set; }
    
    public bool IsRead { get; set; }
    public Message? Message { get; set; }
    public User? User { get; set; }
}