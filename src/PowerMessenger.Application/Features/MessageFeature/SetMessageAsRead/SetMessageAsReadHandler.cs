using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.SetMessageAsRead;

[UsedImplicitly]
public class SetMessageAsReadHandler: IRequestHandler<SetMessageAsReadCommand,SetMessageAsReadResponse>
{
    private readonly IMessageService _messageService;
    private readonly IMessageRepository _messageRepository;

    public SetMessageAsReadHandler(IMessageService messageService, IMessageRepository messageRepository)
    {
        _messageService = messageService;
        _messageRepository = messageRepository;
    }

    public async Task<SetMessageAsReadResponse> Handle(SetMessageAsReadCommand request, CancellationToken cancellationToken)
    {
        await _messageService.SetMessageAsReadAsync(
            request.ChatId,
            request.MessageId,
            request.UserId);

        var unreadMessagesCount = await _messageRepository.GetUnreadMessagesCountChatAsync(
            request.ChatId,
            request.UserId);

        return new SetMessageAsReadResponse(unreadMessagesCount);
    }
}