using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Infrastructure.Shared.Services;

public class ChatService: IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatParticipantsRepository _chatParticipantsRepository;
    private readonly IChatTypeRepository _chatTypeRepository;

    public ChatService(IChatRepository chatRepository, 
        IChatParticipantsRepository chatParticipantsRepository, 
        IChatTypeRepository chatTypeRepository)
    {
        _chatRepository = chatRepository;
        _chatParticipantsRepository = chatParticipantsRepository;
        _chatTypeRepository = chatTypeRepository;
    }

    public async Task<bool> ContainUserInChatAsync(long chatId, long userId)
    {
        return await _chatParticipantsRepository
            .GetChatParticipantInChatAndUserAsync(chatId, userId) is not null;
    }

    public async Task<bool> CheckChatExistenceByIdAsync(long chatId)
    {
        return await _chatRepository
            .GetFirstOfDefaultAsync(p=>p.Id == chatId) is not null;
    }
    
    public async Task<bool> ValidateChatTypeAsync(long chatId, string type)
    {
        var chat = await _chatRepository.GetAsync(p=>p.Id == chatId);

        var chatType = await _chatTypeRepository.GetFirstOfDefaultAsync(p=>p.Id == chat.ChatTypeId);

        return chatType?.Type == type;
    }

    public async Task<bool> CheckExistingChatType(string type)
    {
       return await _chatTypeRepository.GetChatTypeByTypeAsync(type) is not null;
    }
}