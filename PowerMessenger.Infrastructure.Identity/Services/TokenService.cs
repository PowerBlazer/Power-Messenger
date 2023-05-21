using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using PowerMessenger.Infrastructure.Identity.Common;
using PowerMessenger.Infrastructure.Identity.Entities;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity.Services;

public class TokenService: ITokenService
{
    private readonly IIdentityTokenRepository _tokenRepository;

    public TokenService(IIdentityTokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public string GenerateAccessToken(IdentityUser identityUser, JwtOptions options)
    {
        var securityKey = options.GetSymmetricSecurityKey();
        var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email,identityUser.Email!),
            new (JwtRegisteredClaimNames.Sub,identityUser.Id.ToString())
        };

        var token = new JwtSecurityToken(options.Issuer, options.Audience, claims,
            expires: DateTime.Now.AddMinutes(options.AccessExpirationMinutes), signingCredentials: credintials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(long userId, JwtOptions options)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var newRefreshToken = Convert.ToBase64String(randomNumber);

        var newIdentityToken = new IdentityToken
        {
            UserId = userId,
            Token = newRefreshToken,
            Expiration = DateTime.Now.AddDays(options.RefreshExpirationDays)
        };
        
        await _tokenRepository.AddTokenAsync(newIdentityToken);

        return newRefreshToken;
    }

    public async Task<string> UpdateRefreshTokenAsync(long userId, JwtOptions options)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var newRefreshToken = Convert.ToBase64String(randomNumber);

        var refreshIdentityToken = await _tokenRepository.GetTokenByUserId(userId);

        refreshIdentityToken.Token = newRefreshToken;
        refreshIdentityToken.Expiration = DateTime.Now.AddDays(options.RefreshExpirationDays);

        var updatedToken = await _tokenRepository.UpdateTokenAsync(refreshIdentityToken);

        return updatedToken.Token!;
    }
}