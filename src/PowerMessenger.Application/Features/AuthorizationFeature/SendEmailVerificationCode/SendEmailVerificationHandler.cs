using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.SendEmailVerificationCode;

[UsedImplicitly]
public class SendEmailVerificationHandler: IRequestHandler<SendEmailVerificationCommand,string>
{
    private readonly IAuthorizationService _authorizationService;
    
    public SendEmailVerificationHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    public async Task<string> Handle(SendEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        return await _authorizationService.SendEmailVerificationCodeAsync(request.Email!);
    }
}