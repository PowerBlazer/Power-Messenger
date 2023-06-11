namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IChatService
{
    Task<bool> ContainUserInChatAsync(long chatId, long userId);
    Task<bool> CheckChatExistenceByIdAsync(long chatId);
    Task<bool> ValidateChatTypeAsync(long chatId, string type);
}