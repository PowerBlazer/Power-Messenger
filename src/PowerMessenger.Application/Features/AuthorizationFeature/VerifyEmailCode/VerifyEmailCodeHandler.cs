using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.VerifyEmailCode;

[UsedImplicitly]
public class VerifyEmailCodeHandler: IRequestHandler<VerifyEmailCodeCommand>
{
    private readonly IAuthorizationService _authorizationService;

    public VerifyEmailCodeHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task Handle(VerifyEmailCodeCommand request, CancellationToken cancellationToken)
    {
       await _authorizationService.VerifyEmailCodeAsync(request.SessionId, request.VerificationCode);
    }
}