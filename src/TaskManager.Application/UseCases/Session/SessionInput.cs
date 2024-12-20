namespace TaskManager.Application.UseCases.Session;

public class SessionInput : IRequest<SessionOutPut>
{
    public SessionInput(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }



}
