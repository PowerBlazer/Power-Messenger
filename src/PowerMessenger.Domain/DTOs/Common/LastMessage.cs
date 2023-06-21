
namespace PowerMessenger.Domain.DTOs.Common;

public class LastMessage
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? Content { get; set; }
    public required string Type { get; set; }
    public DateTimeOffset DateCreate { get; set; }
}