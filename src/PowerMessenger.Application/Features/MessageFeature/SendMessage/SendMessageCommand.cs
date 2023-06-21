using System.Text.Json.Serialization;
using MediatR;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Application.Features.MessageFeature.SendMessage;

public class SendMessageCommand: IRequest<MessageResponse>
{
    [JsonIgnore]
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public long? ForwardedMessageId { get; set; }
    public required string Type { get; set; }
    public string? Content { get; set; }
    public string? Source { get; set; }
}