using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatRepository
{
    Task<IEnumerable<ChatResponse>> GetChatsByUserAsync(long userId);
    Task<Chat?> GetChatByIdAsync(long chatId);
}