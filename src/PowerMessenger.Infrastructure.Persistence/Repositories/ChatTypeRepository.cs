using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatTypeRepository: IChatTypeRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatTypeRepository(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public async Task<ChatType?> GetChatTypeByIdAsync(long typeId)
    {
        return await _messengerEfContext.ChatTypes
            .FirstOrDefaultAsync(p => p.Id == typeId);
    }
}