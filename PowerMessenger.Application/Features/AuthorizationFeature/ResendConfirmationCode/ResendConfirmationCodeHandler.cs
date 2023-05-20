using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.ResendConfirmationCode;

public class ResendConfirmationCodeHandler: IRequestHandler<ResendConfirmationCodeCommand,string>
{
    private readonly IAuthorizationService _authorizationService;

    public ResendConfirmationCodeHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<string> Handle(ResendConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        return await _authorizationService.ResendVerificationCodeAsync(request.SessionId!, request.Email!);
    }
}