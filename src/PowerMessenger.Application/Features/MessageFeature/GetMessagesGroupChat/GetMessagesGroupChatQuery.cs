using MediatR;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

public record GetMessagesGroupChatQuery(long ChatId,long UserId) : IRequest<IList<MessageGroupChatResponse>>;
