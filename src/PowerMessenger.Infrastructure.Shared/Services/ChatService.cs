using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Infrastructure.Shared.Services;

public class ChatService: IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatParticipantsRepository _chatParticipantsRepository;

    public ChatService(IChatRepository chatRepository, 
        IChatParticipantsRepository chatParticipantsRepository)
    {
        _chatRepository = chatRepository;
        _chatParticipantsRepository = chatParticipantsRepository;
    }

    public async Task<bool> ContainUserInChat(long chatId, long userId)
    {
        return await _chatParticipantsRepository.CheckParticipantInChat(chatId, userId);
    }

    public async Task<bool> CheckChatExistenceById(long chatId)
    {
        return await _chatRepository.GetChatById(chatId) is not null;
    }
}