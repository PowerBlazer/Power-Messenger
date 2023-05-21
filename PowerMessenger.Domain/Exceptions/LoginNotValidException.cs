namespace PowerMessenger.Domain.Exceptions;

public class LoginNotValidException: Exception
{
    public Dictionary<string,List<string>> Errors { get; }

    public LoginNotValidException(string parameterName,params string[] errors)
    {
        Errors = new Dictionary<string, List<string>>
        {
            {
                parameterName,
                errors.ToList()
            }
        };
    }
}