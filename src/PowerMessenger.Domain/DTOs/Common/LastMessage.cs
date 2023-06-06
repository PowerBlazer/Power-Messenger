
namespace PowerMessenger.Domain.DTOs.Common;

public class LastMessage
{
    public string? Content { get; set; }
    public required string Type { get; set; }
    public DateTimeOffset DateCreate { get; set; }
}