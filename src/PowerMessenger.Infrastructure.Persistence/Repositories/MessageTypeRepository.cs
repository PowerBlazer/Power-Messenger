using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageTypeRepository: RepositoryBase<MessageType>, IMessageTypeRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public MessageTypeRepository(IMessengerEfContext messengerEfContext): base(messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public async Task<MessageType?> GetMessageTypeByTypeAsync(string type)
    {
        return await _messengerEfContext.MessageTypes
            .FirstOrDefaultAsync(p => p.Type == type);
    }
}