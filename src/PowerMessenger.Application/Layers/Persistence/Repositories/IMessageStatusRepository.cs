using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageStatusRepository
{
    Task<MessageStatus> UpdateMessageStatusRepositoryAsync(MessageStatus updatedMessageStatus);
    Task<MessageStatus?> GetMessageStatusByChatIdAndUserIdAsync(long chatId, long userId);
    Task<MessageStatus> AddMessageStatusAsync(MessageStatus newMessageStatus);
}