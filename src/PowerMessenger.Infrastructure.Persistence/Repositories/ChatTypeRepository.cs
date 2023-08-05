using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatTypeRepository: RepositoryBase<ChatType>, IChatTypeRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatTypeRepository(IMessengerEfContext messengerEfContext): base(messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }
    
    public async Task<ChatType?> GetChatTypeByTypeAsync(string type)
    {
        return await _messengerEfContext.ChatTypes
            .FirstOrDefaultAsync(p => p.Type == type);
    }
}