using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface IIdentityTokenRepository
{
    Task<IdentityToken> AddTokenAsync(IdentityToken token);
    Task<IdentityToken?> GetTokenByRefreshAsync(string refreshToken);
    Task<IdentityToken> UpdateTokenAsync(IdentityToken newToken);
}