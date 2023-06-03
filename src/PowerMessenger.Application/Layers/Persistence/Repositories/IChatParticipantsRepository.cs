namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatParticipantsRepository
{
    Task<bool> CheckParticipantInChat(long chatId, long userId);
}