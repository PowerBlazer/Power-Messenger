using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

[UsedImplicitly]
public class RegisterUserCommand: IRequest<RegistrationResult>
{
    public string? SessionId { get; set; }
    public string? UserName { get; set; } 
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}