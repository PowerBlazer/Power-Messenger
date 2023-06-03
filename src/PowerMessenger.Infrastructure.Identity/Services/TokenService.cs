﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using PowerMessenger.Application.Layers.Identity;
using PowerMessenger.Domain.Exceptions;
using PowerMessenger.Infrastructure.Identity.Entities;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity.Services;

public class TokenService: ITokenService
{
    private readonly IIdentityTokenRepository _tokenRepository;
    private readonly JwtOptions _jwtOptions;

    public TokenService(IIdentityTokenRepository tokenRepository, 
        JwtOptions jwtOptions)
    {
        _tokenRepository = tokenRepository;
        _jwtOptions = jwtOptions;
    }

    public string GenerateAccessToken(IdentityUser identityUser)
    {
        var securityKey = _jwtOptions.GetSymmetricSecurityKey();
        var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email,identityUser.Email!),
            new (JwtRegisteredClaimNames.Sub,identityUser.Id.ToString())
        };

        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.AccessExpirationMinutes), signingCredentials: credintials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtOptions.Audience,

            ValidateLifetime = false,

            IssuerSigningKey = _jwtOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new AuthenticationValidException("Token", "Не валидный токен доступа");
        }
        
        return principal;
    }

    public async Task<string> GenerateRefreshTokenAsync(long userId)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var newRefreshToken = Convert.ToBase64String(randomNumber);

        var newIdentityToken = new IdentityToken
        {
            UserId = userId,
            Token = newRefreshToken,
            Expiration = DateTime.Now.AddDays(_jwtOptions.RefreshExpirationDays)
        };
        
        await _tokenRepository.AddTokenAsync(newIdentityToken);

        return newRefreshToken;
    }

    public async Task<string> UpdateRefreshTokenAsync(long userId)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var newRefreshToken = Convert.ToBase64String(randomNumber);

        var refreshIdentityToken = await _tokenRepository.GetTokenByUserId(userId);
        
        refreshIdentityToken.Token = newRefreshToken;
        refreshIdentityToken.Expiration = DateTime.Now.AddDays(_jwtOptions.RefreshExpirationDays);

        var updatedToken = await _tokenRepository.UpdateTokenAsync(refreshIdentityToken);

        return updatedToken.Token!;
    }
}