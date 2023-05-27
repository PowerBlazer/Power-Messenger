namespace PowerMessenger.Domain.Exceptions;


public class SessionNotFoundException: Exception
{
    public Dictionary<string,List<string>> Error { get; }

    public SessionNotFoundException(string error)
    {
        Error = new Dictionary<string, List<string>>
        {
            {
                "Session",
                new List<string>
                {
                    error
                }
            }
        };
    }
}