using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Domain.Common;

namespace PowerMessenger.Application.Services;

public interface IAccountService
{
    Task<AuthorizationResult> Register(RegisterDto registerDto);
    Task<AuthorizationResult> Login(LoginDto loginDto);
    Task<bool> ContainEmail(string email);
}