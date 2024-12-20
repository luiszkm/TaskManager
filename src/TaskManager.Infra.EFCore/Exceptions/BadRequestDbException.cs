namespace TaskManager.Infra.EFCore.Exceptions;

public class BadRequestDbException : Exception
{
    public BadRequestDbException(string message) : base(message)
    {
    }
}

