namespace PowerMessenger.Domain.DTOs.Common;

public class ForwardedMessage
{
    public long Id { get; set; }
    public required string UserName { get; set; }
    public string? Content { get; set; }
    public required string Type { get; set; }
}