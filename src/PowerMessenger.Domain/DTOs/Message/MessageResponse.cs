using PowerMessenger.Domain.DTOs.Common;

namespace PowerMessenger.Domain.DTOs.Message;

public class MessageResponse
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public required string Type { get; set; }
    public string? Content { get; set; }
    public string? Source { get; set; }
    public DateTimeOffset DateCreated { get; set; }

    public required MessageOwner MessageOwner { get; set; }
    public ForwardedMessage? ForwardedMessage { get; set; }
}
    
    