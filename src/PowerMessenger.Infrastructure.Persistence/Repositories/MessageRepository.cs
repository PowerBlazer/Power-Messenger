using Dapper;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;
    private readonly IMessengerEfContext _messengerEfContext;

    public MessageRepository(IMessengerDapperContext messengerDapperContext, 
        IMessengerEfContext messengerEfContext)
    {
        _messengerDapperContext = messengerDapperContext;
        _messengerEfContext = messengerEfContext;
    }
    
    //Methods domain models
    public async Task<Message?> GetMessageInTheChatByIdAsync(long messageId, long chatId)
    {
        var message = await _messengerEfContext.Messages
            .FirstOrDefaultAsync(p => p.Id == messageId && p.ChatId == chatId);

        return message;
    }
    
    public async Task<Message> AddMessageAsync(Message message)
    {
        var addedMessages = await _messengerEfContext.Messages
            .AddAsync(message);

        await _messengerEfContext.SaveChangesAsync();

        return addedMessages.Entity;
    }
    
    
    //Methods dto models
    public async Task<MessageResponse?> GetMessageResponseModel(long messageId)
    {
        const string query = @"SELECT 
            id as Id,
            chat_id as ChatId,
            content,
            source,
            date_create as DateCreate,
            type,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_message_data_by_id(@messageId)";

        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var message = await connection.QueryAsync<MessageResponse, MessageOwner, ForwardedMessage, MessageResponse>(
            query,
            (message, owner, forwarded) =>
            {
                message.MessageOwner = owner;
                message.ForwardedMessage = forwarded;
                return message;
            },
            new { messageId = messageId },
            splitOn: "Id,Id"
        );
        
        return message.FirstOrDefault();
    }

   
    
    //Methods common

    public async Task<int> GetUnreadMessagesCountChatAsync(long chatId, long userId)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        const string query = "SELECT * FROM get_unread_message_count(@userId, @chatId)";

        var countUnreadMessages = await connection.QueryFirstOrDefaultAsync<int>(query, new
        {
            userId = userId,
            chatId = chatId
        });

        return countUnreadMessages;
    }

    public async Task<int> GetPrevMessagesCountChatAsync(long chatId, long userId, long messageId)
    {
        var countPrevMessages = await _messengerEfContext.Messages
            .CountAsync(p => p.DateCreate < _messengerEfContext.Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.DateCreate)
                .First() && p.ChatId == chatId && p.DeletedByAll == false && p.DeletedByUserId != userId);
        
        return countPrevMessages;
    }

    public async Task<int> GetNextMessagesCountChatAsync(long chatId, long userId, long messageId)
    {
        var countNextMessages = await _messengerEfContext.Messages
            .CountAsync(p => p.DateCreate > _messengerEfContext.Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.DateCreate)
                .First() && p.ChatId == chatId && p.DeletedByAll == false && p.DeletedByUserId != userId);
        
        return countNextMessages;
    }
}