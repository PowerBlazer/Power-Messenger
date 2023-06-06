using Dapper;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Chat;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.Entities;


namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class ChatRepository: IChatRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;
    private readonly IMessengerEfContext _messengerEfContext;

    public ChatRepository(IMessengerDapperContext messengerDapperContext, 
        IMessengerEfContext messengerEfContext)
    {
        _messengerDapperContext = messengerDapperContext;
        _messengerEfContext = messengerEfContext;
    }

    public async Task<IEnumerable<ChatResponse>> GetChatsByUser(long userId)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();

        #region Request

        var chats = await connection.QueryAsync<ChatResponse,LastMessage,ChatResponse>(
            @"SELECT 
                Id,
                Name,
                datecreate,
                photo,
                description,
                type,
                countparticipants,
                countunreadmessages,
                countmessages,
                lastmessagecontent as Content,
                lastmessagetype as Type,
                lastmessagedatecreate as DateCreate
            FROM get_chats_by_user(@userId)", 
            (summary, message) =>
            {
                summary.LastMessage = message;
                return summary;
            },
            new { userId = userId },
            splitOn:"Content");

        #endregion
        
        return chats;
    }

    public async Task<Chat?> GetChatById(long chatId)
    {
        return await _messengerEfContext.Chats.FirstOrDefaultAsync(p => p.Id == chatId);
    }
}