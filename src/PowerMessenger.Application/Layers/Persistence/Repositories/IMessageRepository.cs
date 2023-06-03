using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUser(long chatId, long userId);
}