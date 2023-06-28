using System.Text.Json.Serialization;

namespace PowerMessenger.Domain.DTOs.Common;

public class MessageOwner
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("userName")]
    public required string UserName { get; set; }
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
}