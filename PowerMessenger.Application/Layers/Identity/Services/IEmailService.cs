namespace PowerMessenger.Application.Layers.Identity.Services;

public interface IEmailService
{
    Task<bool> ContainEmailAsync(string email);
    bool ValidationEmail(string email);
}