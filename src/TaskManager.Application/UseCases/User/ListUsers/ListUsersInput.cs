using TaskManager.Application.UseCases.User.Common;

namespace TaskManager.Application.UseCases.User.ListUsers;

public class ListUsersInput : IRequest<List<UserModelOutput>>
{
    public ListUsersInput(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}
