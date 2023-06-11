using MediatR;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetPrevMessagesGroupChat;

public record GetPrevMessagesGroupChatQuery(long ChatId,long UserId,long MessageId,int Count): IRequest<PrevMessagesGroupChatResponse>;