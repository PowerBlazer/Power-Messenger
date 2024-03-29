﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Domain.DTOs.Common;
using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Infrastructure.Persistence.NpgSetting;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageRepository: RepositoryBase<Message>, IMessageRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;
    private readonly IMessengerEfContext _messengerEfContext;

    public MessageRepository(IMessengerDapperContext messengerDapperContext, 
        IMessengerEfContext messengerEfContext): base(messengerEfContext)
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
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var message = await connection.QueryAsync<MessageResponse, 
            MessageOwner, ForwardedMessage, MessageResponse>(
                NpgFunctionQueries.GetMessageDataById,
                MapOptionMessageResponse,
                new { messageId = messageId },
                splitOn: "Id,Id"
        );
        
        return message.FirstOrDefault();
    }
    //Methods common

    public async Task<int> GetUnreadMessagesCountChatAsync(long chatId, long userId)
    {
        using var connection = _messengerDapperContext.CreateNpgConnection();
        
        var countUnreadMessages = await connection.QueryFirstOrDefaultAsync<int>(
            NpgFunctionQueries.GetUnreadMessagesCount, 
            new 
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


    private static MessageResponse MapOptionMessageResponse(MessageResponse message,
        MessageOwner owner,
        ForwardedMessage forwardedMessage)
    {
        message.MessageOwner = owner;
        message.ForwardedMessage = forwardedMessage;
        return message;
    }
}