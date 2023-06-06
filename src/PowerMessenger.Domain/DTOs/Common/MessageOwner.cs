namespace PowerMessenger.Domain.DTOs.Common;

public class MessageOwner
{
    public long Id { get; set; }
    public required string UserName { get; set; }
    public string? Avatar { get; set; }
}