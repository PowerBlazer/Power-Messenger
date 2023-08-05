using Dapper;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;
using PowerMessenger.Infrastructure.Persistence.NpgSetting;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageGroupChatRepository: IMessageGroupChatRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;

    public MessageGroupChatRepository(IMessengerDapperContext messengerDapperContext)
    {
        _messengerDapperContext = messengerDapperContext;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUserAsync(long chatId, long userId,
        int next,int prev)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,
            MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
                NpgFunctionQueries.GetMessagesGroupChatByUser, 
                MapOptionGroupChatMessages,
                new
                {
                    chatId = chatId,
                    userId = userId,
                    next = next,
                    prev = prev
                }, splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetNextMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,
            MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
                NpgFunctionQueries.GetNextMessagesGroupChatByUser, 
                MapOptionGroupChatMessages,
                new
                {
                    chatId = chatId,
                    userId = userId,
                    messageId = messageId,
                    count = count
                }, splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetPrevMessagesGroupChatByUserAsync(long chatId, long userId,
        long messageId, int count)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,
            MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
                NpgFunctionQueries.GetPrevMessagesGroupChatByUser,
                MapOptionGroupChatMessages,
                new
                {
                    chatId = chatId,
                    userId = userId,
                    messageId = messageId,
                    count = count
                }, splitOn: "Id,Id");

        return messagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetLastMessagesGroupChatByUserAsync(long chatId, 
        long userId, int count)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var lastMessagesGroup = await connection.QueryAsync<MessageGroupChatResponse,
            MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
                NpgFunctionQueries.GetLastMessagesGroupChatByUser, 
                MapOptionGroupChatMessages,
                new
                {
                    chatId = chatId,
                    userId = userId,
                    count = count
                }, splitOn: "Id,Id");

        return lastMessagesGroup;
    }

    public async Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByMessageId(long chatId, long messageId, 
        long userId, int next, int prev)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var messagesGroup = await connection.QueryAsync<MessageGroupChatResponse,
            MessageOwner,ForwardedMessage,MessageGroupChatResponse>(
                NpgFunctionQueries.GetMessagesGroupChatByMessageId, 
                MapOptionGroupChatMessages,
                new
                {
                    chatId = chatId,
                    userId = userId,
                    messageId = messageId,
                    next = next,
                    prev = prev
                }, splitOn: "Id,Id");

        return messagesGroup;
    }

    private static MessageGroupChatResponse MapOptionGroupChatMessages(MessageGroupChatResponse message,
        MessageOwner owner, ForwardedMessage forwardedMessage)
    {
        message.MessageOwner = owner;
        message.ForwardedMessage = forwardedMessage;
        return message;
    }
}