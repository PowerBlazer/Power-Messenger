using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatRepository: IRepository<Chat>
{
    Task<IEnumerable<Chat>> GetChatsByUserAsync(long userId);
    Task<IEnumerable<ChatResponse>> GetChatsDataByUserAsync(long userId);
}