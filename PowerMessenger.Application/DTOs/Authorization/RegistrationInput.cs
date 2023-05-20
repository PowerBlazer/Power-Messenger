using JetBrains.Annotations;

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

    public string SessionId { get; }
    public string UserName { get; } 
    public string Password { get; }
}