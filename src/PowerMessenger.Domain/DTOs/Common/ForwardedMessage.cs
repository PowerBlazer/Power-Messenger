using System.Text.Json.Serialization;

namespace PowerMessenger.Domain.DTOs.Common;

public class ForwardedMessage
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("userName")]
    public required string UserName { get; set; }
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("chatId")]
    public long ChatId { get; set; }
}