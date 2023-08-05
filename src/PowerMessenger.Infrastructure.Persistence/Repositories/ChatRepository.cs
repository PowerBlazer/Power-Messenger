using Dapper;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Infrastructure.Persistence.NpgSetting;


namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatRepository: RepositoryBase<Chat>, IChatRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatRepository(IMessengerDapperContext messengerDapperContext, 
        IMessengerEfContext messengerEfContext): base(messengerEfContext)
    {
        _messengerDapperContext = messengerDapperContext;
        _messengerEfContext = messengerEfContext;
    }

    public async Task<IEnumerable<ChatResponse>> GetChatsDataByUserAsync(long userId)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var chats = await connection.QueryAsync<ChatResponse,LastMessage,ChatResponse>(
            NpgFunctionQueries.GetChatsDataByUserId, 
            MapOptionChatResponse,
            new { userId = userId },
            splitOn:"Id");
        
        return chats;
    }

    public async Task<IEnumerable<Chat>> GetChatsByUserAsync(long userId)
    {
        return await _messengerEfContext.Chats
            .Where(p => p.ChatParticipants!.FirstOrDefault(x => x.UserId == userId) != null)
            .ToListAsync();
       
    }
    
    private static ChatResponse MapOptionChatResponse(ChatResponse chatResponse, LastMessage lastMessage)
    {
        chatResponse.LastMessage = lastMessage;
        return chatResponse;
    }
}