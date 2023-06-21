using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageGroupChatRepository
{
    Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUserAsync(long chatId, long userId,
        int next,int prev);
    Task<IEnumerable<MessageGroupChatResponse>> GetNextMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count);
    Task<IEnumerable<MessageGroupChatResponse>> GetPrevMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count);
    Task<IEnumerable<MessageGroupChatResponse>> GetLastMessagesGroupChatByUserAsync(long chatId, long userId,
        int count);
    Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByMessageId(long chatId, long messageId,
        long userId, int next, int prev);
}