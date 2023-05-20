using JetBrains.Annotations;
using MediatR;

namespace PowerMessenger.Application.Features.AuthorizationFeature.ResendConfirmationCode;

[UsedImplicitly]
public class ResendConfirmationCodeCommand: IRequest<string>
{
    public string? SessionId { get; set; }
    public string? Email { get; set; }
}