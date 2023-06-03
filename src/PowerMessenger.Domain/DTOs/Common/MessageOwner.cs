namespace PowerMessenger.Domain.DTOs.Common;

public class MessageOwner
{
    public required string UserName { get; set; }
    public string? Avatar { get; set; }
}