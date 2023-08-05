using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IMessageTypeRepository: IRepository<MessageType>
{
    Task<MessageType?> GetMessageTypeByTypeAsync(string type);
}