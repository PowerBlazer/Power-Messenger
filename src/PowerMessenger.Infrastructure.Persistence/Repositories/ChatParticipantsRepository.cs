using PowerMessenger.Application.Layers.Persistence.Repositories;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatParticipantsRepository: IChatParticipantsRepository
{
    public Task<bool> CheckParticipantInChat(long chatId, long userId)
    {
        throw new NotImplementedException();
    }
}