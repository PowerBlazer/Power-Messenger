using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

public class Message : BaseEntity<long>
{
    public string? Content { get; set; }
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public DateTime DateCreate { get; set; }
    public long MessageTypeId { get; set; }
    public long? ForwardedMessageId { get; set; }
    public string? Source { get; set; }
    public long? DeletedByUserId { get; set; }
    public bool DeletedByAll { get; set; }
    
    
    public Message? ForwardMessage { get; set; }
    public MessageType? MessageType { get; set; }
    public Chat? Chat { get; set; }
    public User? User { get; set; }
    public User? DeletedByUser { get; set; }
    public ICollection<MessageStatus>? MessageStatuses { get; set; }
    
    

}