namespace PowerMessenger.Application.DTOs.Authorization;

public class LoginInput
{
    public LoginInput(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; }
}