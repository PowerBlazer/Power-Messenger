
namespace PowerMessenger.Application.DTOs.Authorization;

public class RegistrationInput
{
    public RegistrationInput(string sessionId, string userName, 
        string password)
    {
        SessionId = sessionId;
        UserName = userName;
        Password = password;
    }
    
    /// <summary>
    /// ID сессии
    /// </summary>
    public string SessionId { get; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; } 
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; }
}