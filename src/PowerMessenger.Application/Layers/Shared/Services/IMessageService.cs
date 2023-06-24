namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IMessageService
{
    Task<bool> ContainMessageByMessageId(long messageId);
    Task<bool> ContainMessageInChatAsync(long messageId, long chatId);
    Task<bool> CheckExistingMessageType(string type);
    Task SetMessageAsReadAsync(long chatId, long messageId, long userId);
}