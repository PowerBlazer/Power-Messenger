using MediatR;
using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;

public class LoginUserHandler: IRequestHandler<LoginUserCommand,LoginResult>
{
    public Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}