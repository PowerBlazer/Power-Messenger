using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageRepository
{
    Task<Message?> GetMessageInTheChatByIdAsync(long messageId, long chatId);
    Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUserAsync(long chatId, long userId,int next,int prev);
    Task<IEnumerable<MessageGroupChatResponse>> GetNextMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count);
    Task<IEnumerable<MessageGroupChatResponse>> GetPrevMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count);
    Task<int> GetCountUnreadMessagesChatAsync(long chatId, long userId);
    Task<int> GetCountPrevMessagesChatAsync(long chatId, long userId, long messageId);
    Task<int> GetCountNextMessagesChatAsync(long chatId, long userId, long messageId);
}