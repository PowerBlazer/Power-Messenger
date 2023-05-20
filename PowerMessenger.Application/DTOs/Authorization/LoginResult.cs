﻿namespace PowerMessenger.Application.DTOs.Authorization;

public class LoginResult
{
    public LoginResult(string accessToken, 
        string refreshToken)
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