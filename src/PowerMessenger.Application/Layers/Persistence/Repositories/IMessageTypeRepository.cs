using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageTypeRepository
{
    Task<MessageType?> GetMessageTypeByTypeAsync(string type);
}