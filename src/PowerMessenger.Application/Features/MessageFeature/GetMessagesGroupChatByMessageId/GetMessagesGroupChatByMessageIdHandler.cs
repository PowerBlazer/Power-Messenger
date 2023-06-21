using MediatR;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChatByMessageId;

public class GetMessagesGroupChatByMessageIdHandler: 
    IRequestHandler<GetMessagesGroupChatByMessageIdQuery,MessagesGroupChatResponse>
{
    public Task<MessagesGroupChatResponse> Handle(GetMessagesGroupChatByMessageIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}