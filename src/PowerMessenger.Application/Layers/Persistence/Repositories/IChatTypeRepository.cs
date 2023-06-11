using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatTypeRepository
{
    Task<ChatType?> GetChatTypeByIdAsync(long typeId);
}