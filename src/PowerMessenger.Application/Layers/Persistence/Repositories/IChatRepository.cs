using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatRepository
{
    Task<IEnumerable<Chat>> GetChatsByUserAsync(long userId);
    Task<Chat?> GetChatByIdAsync(long chatId);
    Task<IEnumerable<ChatResponse>> GetChatsDataByUserAsync(long userId);
}