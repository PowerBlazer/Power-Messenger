namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IChatService
{
    Task<bool> ContainUserInChat(long chatId, long userId);
    Task<bool> CheckChatExistenceById(long chatId);
}