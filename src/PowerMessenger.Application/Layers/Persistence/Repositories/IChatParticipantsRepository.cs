using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatParticipantsRepository
{
    Task<ChatParticipant?> GetChatParticipantInChatAndUserAsync(long chatId, long userId);
}