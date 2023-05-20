namespace PowerMessenger.Domain.Exceptions;


public class SessionNotFoundException: Exception
{
    public string? Error { get; }

    public SessionNotFoundException(string error)
    {
        Error = error;
    }
}