using TaskManager.Application.UseCases.User.Common;

namespace TaskManager.Application.UseCases.User.Create;
public class CreateUserInput : IRequest<UserModelOutput>
{
    public CreateUserInput(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}
