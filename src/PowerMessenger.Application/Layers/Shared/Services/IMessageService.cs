namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IMessageService
{
    Task<bool> ContainMessageInChat(long messageId, long chatId);
}