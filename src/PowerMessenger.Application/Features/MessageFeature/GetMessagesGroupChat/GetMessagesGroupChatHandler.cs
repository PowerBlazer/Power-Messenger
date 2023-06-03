using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

public class GetChatsByUserHandler: IRequestHandler<GetMessagesGroupChatQuery,IList<MessageGroupChatResponse>>
{
    private readonly IMessageRepository _messageRepository;

    public GetChatsByUserHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<IList<MessageGroupChatResponse>> Handle(GetMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
       var messagesEnumerable = await _messageRepository.GetMessagesGroupChatByUser(request.ChatId, request.UserId);

       return messagesEnumerable.ToList();
    }
}