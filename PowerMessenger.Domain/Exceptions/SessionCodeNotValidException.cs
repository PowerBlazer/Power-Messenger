namespace PowerMessenger.Domain.Exceptions;

public class SessionCodeNotValidException: Exception
{
    public string? Error { get; }

    public SessionCodeNotValidException(string error)
    {
        Error = error;
    }

    
}