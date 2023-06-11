namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatParticipantsRepository
{
    Task<bool> CheckParticipantInChatAsync(long chatId, long userId);
}