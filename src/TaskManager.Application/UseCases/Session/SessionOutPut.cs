namespace TaskManager.Application.UseCases.Session;

public class SessionOutPut
{
    public SessionOutPut(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }

    public Guid UserId { get; set; }
    public string Token { get; set; }

}
