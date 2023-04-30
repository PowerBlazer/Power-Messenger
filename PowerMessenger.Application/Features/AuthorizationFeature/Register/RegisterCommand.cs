using MediatR;
using PowerMessenger.Domain.Common;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Register;

public class RegisterCommand: IRequest<AuthorizationResult>
{
    public string? Email { get; set; } 
    public string? UserName { get; set; } 
    public string? Password { get; set; } 
    public string? PhoneNumber { get; set; }
}