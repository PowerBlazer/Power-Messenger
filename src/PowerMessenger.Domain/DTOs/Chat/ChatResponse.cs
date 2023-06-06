using PowerMessenger.Domain.DTOs.Common;

namespace PowerMessenger.Domain.DTOs.Chat;

public class ChatResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset DateCreate { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
    public required string Type { get; set; }
    public int CountParticipants { get; set; }
    public int CountUnreadMessages { get; set; }
    public int CountMessages { get; set; }
    public LastMessage? LastMessage { get; set; }
}