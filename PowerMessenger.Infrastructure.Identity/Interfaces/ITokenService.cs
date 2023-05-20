using PowerMessenger.Infrastructure.Identity.Common;
using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IdentityUser identityUser, JwtOptions options);
    Task<string> GenerateRefreshTokenAsync(long userId, JwtOptions options);
}