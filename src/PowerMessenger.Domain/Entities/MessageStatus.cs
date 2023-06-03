using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class MessageStatus : BaseEntity<long>
{
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public long? LastMessageReadId { get; set; }
    public Message? Message { get; set; }
    public User? User { get; set; }
    public Chat? Chat { get; set; }
}