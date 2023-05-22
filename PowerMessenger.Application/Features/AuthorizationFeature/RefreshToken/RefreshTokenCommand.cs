using MediatR;
using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;

public class RefreshTokenCommand: IRequest<RefreshTokenResult>
{
    public RefreshTokenCommand(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; }
    /// <summary>
    /// Токен обновления
    /// </summary>
    public string RefreshToken { get; }
}