using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetLastMessagesGroupChat;

public class GetLastMessagesGroupChatHandler: IRequestHandler<GetLastMessagesGroupChatQuery,MessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageGroupChatRepository _messageGroupChatRepository;

    public GetLastMessagesGroupChatHandler(IMessageRepository messageRepository, 
        IMessageGroupChatRepository messageGroupChatRepository)
    {
        _messageRepository = messageRepository;
        _messageGroupChatRepository = messageGroupChatRepository;
    }

    public async Task<MessagesGroupChatResponse> Handle(GetLastMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
        var lastMessagesGroup = (await _messageGroupChatRepository
            .GetLastMessagesGroupChatByUserAsync(
                request.ChatId,
                request.UserId,
                request.Count
            )).ToList();
        
        var prevMessagesCount = 0;
        if (lastMessagesGroup.Count > 0)
        {
            prevMessagesCount = await _messageRepository.GetPrevMessagesCountChatAsync(
                request.ChatId,
                request.UserId,
                lastMessagesGroup.First().Id);
        }

        return new MessagesGroupChatResponse(lastMessagesGroup, 0, prevMessagesCount, 0);
    }
}