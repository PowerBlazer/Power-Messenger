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

    public async Task<bool> ContainUserInChat(long chatId, long userId)
    {
        return await _chatParticipantsRepository.CheckParticipantInChat(chatId, userId);
    }

    public async Task<bool> CheckChatExistenceById(long chatId)
    {
        return await _chatRepository.GetChatById(chatId) is not null;
    }

    public async Task<bool> ValidateChatType(long chatId, string type)
    {
        var chat = await _chatRepository.GetChatById(chatId);

        var chatType = await _chatTypeRepository.GetChatTypeById(chat!.ChatTypeId);

        return chatType?.Type == type;
    }
}