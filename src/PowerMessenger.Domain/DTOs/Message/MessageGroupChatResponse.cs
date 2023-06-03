using PowerMessenger.Domain.DTOs.Common;

namespace PowerMessenger.Domain.DTOs.Message;

public class MessageGroupChatResponse
{
    public long Id { get; set; }
    public string? Content { get; set; }
    public DateTime DateCreate { get; set; }
    public required string Type { get; set; }
    public string? Source { get; set; }
    public bool IsOwner { get; set; }
    public bool IsRead { get; set; }
    public required MessageOwner MessageOwner { get; set; }
    public ForwardedMessage? ForwardedMessage { get; set; }
}