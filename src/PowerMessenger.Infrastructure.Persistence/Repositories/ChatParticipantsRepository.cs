using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatParticipantsRepository: IChatParticipantsRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatParticipantsRepository(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public async Task<bool> CheckParticipantInChatAsync(long chatId, long userId)
    {
        return await _messengerEfContext.ChatParticipants
            .FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId) is not null;
    }
}