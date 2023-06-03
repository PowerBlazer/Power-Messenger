using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatRepository
{
    Task<IEnumerable<ChatResponse>> GetChatsByUser(long userId);
    Task<Chat?> GetChatById(long chatId);
}