using JetBrains.Annotations;

namespace PowerMessenger.Domain.Exceptions;


public class SessionExceptionNotFound: Exception
{
    public string? Error { get; }

    public SessionExceptionNotFound(string error)
    {
        Error = error;
    }
}