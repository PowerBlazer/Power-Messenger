using MediatR;
using Microsoft.AspNetCore.SignalR;
using PowerMessenger.Application.Hubs;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.Entities;
using PowerMessenger.Domain.Enums;

namespace PowerMessenger.Application.Features.MessageFeature.SendMessage;

public class SendMessageHandler: IRequestHandler<SendMessageCommand,MessageResponse>
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageTypeRepository _messageTypeRepository;

    public SendMessageHandler(IHubContext<ChatHub> hubContext, 
        IMessageRepository messageRepository, 
        IMessageTypeRepository messageTypeRepository)
    {
        _hubContext = hubContext;
        _messageRepository = messageRepository;
        _messageTypeRepository = messageTypeRepository;
    }

    public async Task<MessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var messageType = await _messageTypeRepository.GetMessageTypeByTypeAsync(request.Type);
        var addMessage = await _messageRepository.AddMessageAsync(new Message
        {
            ChatId = request.ChatId,
            UserId = request.UserId,
            MessageTypeId = messageType!.Id,
            Content = request.Content,
            Source = request.Source,
            DateCreate = DateTimeOffset.UtcNow,
            ForwardedMessageId = request.ForwardedMessageId
        });

        var messageResponse = await _messageRepository
                .GetMessageResponseModel(addMessage.Id);

        await _hubContext.Clients.GroupExcept(request.ChatId.ToString(),request.UserId.ToString())
            .SendAsync(HubResponseEndpoints.Receive.ToString(), 
                messageResponse?.ToString(),
                request.UserId,
                cancellationToken);
        
        return messageResponse!;
    }
}