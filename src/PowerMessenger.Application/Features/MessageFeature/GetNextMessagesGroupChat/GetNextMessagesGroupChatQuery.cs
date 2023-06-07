using MediatR;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetNextMessagesGroupChat;

public record GetNextMessagesGroupChatQuery(long ChatId,long UserId,long MessageId,int Count): IRequest<NextMessagesGroupChatResponse>;