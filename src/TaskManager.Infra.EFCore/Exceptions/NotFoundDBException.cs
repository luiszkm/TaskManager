namespace TaskManager.Infra.EFCore.Exceptions;

public class NotFoundDBException : Exception
{
    public NotFoundDBException(string message) : base(message)
    {

    }

}
