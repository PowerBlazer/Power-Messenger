using PowerMessenger.Application.DTOs.Authorization;

namespace PowerMessenger.Application.Layers.Identity.Services;

public interface IAuthorizationService
{
    /// <summary>
    /// Отправка кода подтверждения на почту пользователя
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <returns>Возвращает ID созданной сессии</returns>
    Task<string> SendEmailVerificationCodeAsync(string email);

    /// <summary>
    /// Повторная отправка кода подтверждения на почту пользователя
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="email">Почта пользователя</param>
    /// <returns>Возвращает ID пересозданной сессии</returns>
    Task<string> ResendVerificationCodeAsync(string sessionId,string email);
    /// <summary>
    /// Подтверждения почты пользователя с кодом подтверждения
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="verifyCode">Код подтверждения</param>
    /// <returns></returns>
    Task VerifyEmailCodeAsync(string sessionId,string verifyCode);
    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="registrationInput"></param>
    /// <returns>Возвращает результат регистрации</returns>
    Task<RegistrationResult> RegisterUserAsync(RegistrationInput registrationInput);
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="loginInput"></param>
    /// <returns>Возвращает результат авторизации</returns>
    Task<LoginResult> LoginUserAsync(LoginInput loginInput);
    /// <summary>
    /// Обновление токенов
    /// </summary>
    /// <param name="refreshTokenInput">Объект токен доступа и обновления</param>
    /// <returns>Возвращает обновленные токены</returns>
    Task<RefreshTokenResult> RefreshToken(RefreshTokenInput refreshTokenInput);
}