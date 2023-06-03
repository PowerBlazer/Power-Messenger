using PowerMessenger.Domain.DTOs.Common;

namespace PowerMessenger.Domain.DTOs.Message;

public class MessageGroupChatResponse
{
    public long Id { get; set; }
    public string? Content { get; set; }
    public DateTime DateCreate { get; set; }
    public required MessageOwner MessageOwner { get; set; }
}