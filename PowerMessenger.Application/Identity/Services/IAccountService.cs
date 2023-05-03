using PowerMessenger.Application.Common;
using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Identity.Services;

public interface IAccountService
{
    Task<AuthorizationResult> Register(RegisterDto registerDto);
    Task<AuthorizationResult> Login(LoginDto loginDto);
    Task<bool> ContainEmail(string email);
}