using PowerMessenger.Application.Common;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Identity.Services;

namespace PowerMessenger.Infrastructure.Identity.Services;

public class AccountService: IAccountService
{
    public Task<AuthorizationResult> Register(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorizationResult> Login(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ContainEmail(string email)
    {
        throw new NotImplementedException();
    }
}