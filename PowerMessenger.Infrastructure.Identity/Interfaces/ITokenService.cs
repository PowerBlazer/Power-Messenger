using System.Security.Claims;
using PowerMessenger.Application.Layers.Identity;
using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IdentityUser identityUser);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    Task<string> GenerateRefreshTokenAsync(long userId);
    Task<string> UpdateRefreshTokenAsync(long userId);
    
}