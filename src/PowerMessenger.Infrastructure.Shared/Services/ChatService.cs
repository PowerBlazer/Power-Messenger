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
        return await _chatParticipantsRepository.CheckParticipantInChatAsync(chatId, userId);
    }

    public async Task<bool> CheckChatExistenceByIdAsync(long chatId)
    {
        return await _chatRepository.GetChatByIdAsync(chatId) is not null;
    }

    public async Task<bool> ValidateChatTypeAsync(long chatId, string type)
    {
        var chat = await _chatRepository.GetChatByIdAsync(chatId);

        var chatType = await _chatTypeRepository.GetChatTypeByIdAsync(chat!.ChatTypeId);

        return chatType?.Type == type;
    }
}