using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

[UsedImplicitly]
public class GetChatsByUserHandler: IRequestHandler<GetMessagesGroupChatQuery,MessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetChatsByUserHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessagesGroupChatResponse> Handle(GetMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
       var messagesEnumerable = await _messageRepository.GetMessagesGroupChatByUser(
           request.ChatId,
           request.UserId,
           request.Next,
           request.Prev
       );
       
       var countUnreadMessages = await _messageRepository.GetCountUnreadMessagesChat(request.ChatId, request.UserId);

       return new MessagesGroupChatResponse(messagesEnumerable.ToList(),countUnreadMessages);
    }
}