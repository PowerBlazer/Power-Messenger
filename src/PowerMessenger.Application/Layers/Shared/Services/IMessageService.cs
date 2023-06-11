namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IMessageService
{
    Task<bool> ContainMessageInChatAsync(long messageId, long chatId);
    Task SetMessageAsReadAsync(long chatId, long messageId, long userId);
}