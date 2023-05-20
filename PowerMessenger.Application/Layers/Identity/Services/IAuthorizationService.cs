using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Layers.Identity.Services;

public interface IAuthorizationService
{
    Task<string> SendEmailVerificationCodeAsync(string email);
    Task<string> ResendVerificationCodeAsync(string sessionId,string email);
    Task VerifyEmailCodeAsync(string sessionId,string verifyCode);
    Task<RegistrationResult> RegisterUserAsync(RegistrationInput registrationInput);
}