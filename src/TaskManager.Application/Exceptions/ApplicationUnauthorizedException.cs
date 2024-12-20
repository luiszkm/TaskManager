namespace TaskManager.Application.Exceptions;

public class ApplicationUnauthorizedException : Exception
{
    public ApplicationUnauthorizedException(string message) : base(message)
    {
    }
}
