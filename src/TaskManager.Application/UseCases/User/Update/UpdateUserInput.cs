using TaskManager.Application.UseCases.User.Common;

namespace TaskManager.Application.UseCases.User.Update;

public class UpdateUserInput : IRequest<UserModelOutput>
{
    public UpdateUserInput(Guid id, string password)
    {
        Id = id;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }

}
