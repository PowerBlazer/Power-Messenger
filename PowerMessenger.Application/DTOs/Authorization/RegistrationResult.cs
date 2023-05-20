namespace PowerMessenger.Application.DTOs.Authorization;

public class RegistrationResult
{
    public RegistrationResult(string? accessToken, string? refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string? AccessToken { get; }
    public string? RefreshToken { get; }
}