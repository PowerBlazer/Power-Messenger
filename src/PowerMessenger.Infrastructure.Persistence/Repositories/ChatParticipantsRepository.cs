using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatParticipantsRepository: IChatParticipantsRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatParticipantsRepository(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }
    
    public async Task<ChatParticipant?> GetChatParticipantInChatAndUserAsync(long chatId, long userId)
    {
        return await _messengerEfContext.ChatParticipants
            .FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId);
    }
}