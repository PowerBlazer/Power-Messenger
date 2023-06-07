using Dapper;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Common;
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

    public async Task<Message?> GetMessageInTheChatById(long messageId, long chatId)
    {
        var message = await _messengerEfContext.Messages
            .FirstOrDefaultAsync(p => p.Id == messageId && p.ChatId == chatId);

        return message;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUser(long chatId, long userId,
        int next,int prev)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        const string query = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type
        FROM get_messages_group_chat_by_user(@chatId,@userId,@next,@prev)";

        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
            query,
            (message, owner, forwarded) =>
            {
                message.MessageOwner = owner;
                message.ForwardedMessage = forwarded;
                return message;
            },
            new
            {
                chatId = chatId,
                userId = userId,
                next = next,
                prev = prev
            },
            splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetNextMessagesGroupChatByUser(long chatId, long userId,
        long messageId, int count)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        const string query = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type
        FROM get_next_messages_group_chat_by_user(@chatId,@userId,@count)";

        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
            query,
            (message, owner, forwarded) =>
            {
                message.MessageOwner = owner;
                message.ForwardedMessage = forwarded;
                return message;
            },
            new
            {
                chatId = chatId,
                userId = userId,
                count = count
            },
            splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetPrevMessagesGroupChatByUser(long chatId, long userId,
        long messageId, int count)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        const string query = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type
        FROM get_prev_messages_group_chat_by_user(@chatId,@userId,@messageId,@count)";

        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
            query,
            (message, owner, forwarded) =>
            {
                message.MessageOwner = owner;
                message.ForwardedMessage = forwarded;
                return message;
            },
            new
            {
                chatId = chatId,
                userId = userId,
                messageId = messageId,
                count = count
            },
            splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<int> GetCountUnreadMessagesChat(long chatId, long userId)
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

    public async Task<int> GetCountPrevMessagesChat(long chatId, long userId, long messageId)
    {
        var countPrevMessages = await _messengerEfContext.Messages
            .CountAsync(p => p.DateCreate < _messengerEfContext.Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.DateCreate)
                .First() && p.DeletedByAll == false && p.DeletedByUserId != userId);
        
        return countPrevMessages;
    }

    public async Task<int> GetCountNextMessagesChat(long chatId, long userId, long messageId)
    {
        var countNextMessages = await _messengerEfContext.Messages
            .CountAsync(p => p.DateCreate > _messengerEfContext.Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.DateCreate)
                .First() && p.DeletedByAll == false && p.DeletedByUserId != userId);
        
        return countNextMessages;
    }
}