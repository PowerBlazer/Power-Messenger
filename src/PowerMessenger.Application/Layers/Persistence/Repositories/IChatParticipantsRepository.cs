using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatParticipantsRepository: IRepository<ChatParticipant>
{
    Task<ChatParticipant?> GetChatParticipantInChatAndUserAsync(long chatId, long userId);
}