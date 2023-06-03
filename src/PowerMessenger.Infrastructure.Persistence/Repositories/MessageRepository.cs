using Dapper;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;

    public MessageRepository(IMessengerDapperContext messengerDapperContext)
    {
        _messengerDapperContext = messengerDapperContext;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUser(long chatId, long userId)
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
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type
        FROM get_messages_group_chat_by_user(@chatId,@userId)";

        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
            query,
            (message, owner, forwarded) =>
            {
                message.MessageOwner = owner;
                message.ForwardedMessage = forwarded;
                return message;
            },
            new { chatId = chatId,userId = userId },
            splitOn: "UserName,Id");

        return messagesGroup;
    }
}