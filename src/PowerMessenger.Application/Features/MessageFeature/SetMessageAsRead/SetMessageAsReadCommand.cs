using MediatR;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.SetMessageAsRead;

public record SetMessageAsReadCommand(long ChatId,long MessageId,long UserId): IRequest<SetMessageAsReadResponse>;
