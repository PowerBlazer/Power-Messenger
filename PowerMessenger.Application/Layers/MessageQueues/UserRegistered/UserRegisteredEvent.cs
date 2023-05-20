namespace PowerMessenger.Application.Layers.MessageQueues.UserRegistered;

public class UserRegisteredEvent
{
    public long UserId { get; set; }
    public string? UserName { get; set; }
}