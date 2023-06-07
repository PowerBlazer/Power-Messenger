using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageRepository
{
    Task<Message?> GetMessageInTheChatById(long messageId, long chatId);
    Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUser(long chatId, long userId,int next,int prev);
    Task<IEnumerable<MessageGroupChatResponse>> GetNextMessagesGroupChatByUser(long chatId, long userId,
        long messageId, int count);
    Task<IEnumerable<MessageGroupChatResponse>> GetPrevMessagesGroupChatByUser(long chatId, long userId,
        long messageId, int count);
    Task<int> GetCountUnreadMessagesChat(long chatId, long userId);
    Task<int> GetCountPrevMessagesChat(long chatId, long userId, long messageId);
    Task<int> GetCountNextMessagesChat(long chatId, long userId, long messageId);
}