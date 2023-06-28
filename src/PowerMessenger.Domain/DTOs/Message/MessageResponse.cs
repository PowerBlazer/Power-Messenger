using System.Text.Json;
using System.Text.Json.Serialization;
using PowerMessenger.Domain.DTOs.Common;

namespace PowerMessenger.Domain.DTOs.Message;

public class MessageResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("chatId")]
    public long ChatId { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("source")]
    public string? Source { get; set; }
    [JsonPropertyName("dateCreated")]
    public DateTimeOffset DateCreated { get; set; }

    [JsonPropertyName("messageOwner")]
    public required MessageOwner MessageOwner { get; set; }
    [JsonPropertyName("forwardedMessage")]
    public ForwardedMessage? ForwardedMessage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
    
    