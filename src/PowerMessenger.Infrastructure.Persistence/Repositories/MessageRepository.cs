using Dapper;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.DTOs.Message;

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
}