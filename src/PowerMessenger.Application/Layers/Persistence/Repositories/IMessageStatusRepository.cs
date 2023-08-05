using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageStatusRepository: IRepository<MessageStatus>
{
    Task<MessageStatus> UpdateMessageStatusRepositoryAsync(MessageStatus updatedMessageStatus);
    Task<MessageStatus?> GetMessageStatusByChatIdAndUserIdAsync(long chatId, long userId);
}