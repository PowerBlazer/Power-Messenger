using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageStatusRepository: IMessageStatusRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public MessageStatusRepository(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public async Task<MessageStatus> UpdateMessageStatusRepositoryAsync(MessageStatus updatedMessageStatus)
    {
        _messengerEfContext.MessageStatuses.Attach(updatedMessageStatus);
        _messengerEfContext.Entry(updatedMessageStatus).State = EntityState.Modified;

        await _messengerEfContext.SaveChangesAsync();

        return updatedMessageStatus;
    }

    public async Task<MessageStatus?> GetMessageStatusByChatIdAndUserIdAsync(long chatId, long userId)
    {
        var messageStatus = await _messengerEfContext.MessageStatuses
            .FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId);

        return messageStatus;
    }

    public async Task<MessageStatus> AddMessageStatusAsync(MessageStatus newMessageStatus)
    {
        var messageStatus = await _messengerEfContext.MessageStatuses
            .AddAsync(newMessageStatus);

        return messageStatus.Entity;
    }
}