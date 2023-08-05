using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageRepository: IRepository<Message>
{
    Task<Message?> GetMessageInTheChatByIdAsync(long messageId, long chatId);
    Task<MessageResponse?> GetMessageResponseModel(long messageId);
    Task<int> GetUnreadMessagesCountChatAsync(long chatId, long userId);
    Task<int> GetPrevMessagesCountChatAsync(long chatId, long userId, long messageId);
    Task<int> GetNextMessagesCountChatAsync(long chatId, long userId, long messageId);
}