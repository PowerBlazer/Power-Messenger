using MediatR;
using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;

public class LoginUserCommand: IRequest<LoginResult>
{
    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }
}