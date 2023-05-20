using JetBrains.Annotations;
using MediatR;

namespace PowerMessenger.Application.Features.AuthorizationFeature.VerifyEmailCode;

[UsedImplicitly]
public class VerifyEmailCodeCommand: IRequest
{
    public string? SessionId { get; set; }
    public string? VerificationCode { get; set; }
}