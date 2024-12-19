using TaskManager.Application.UseCases.User.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.User.ListUsers;

public class ListUsers : IRequestHandler<ListUsersInput, List<UserModelOutput>>
{
    private readonly IUserRepository _userRepository;

    public ListUsers(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserModelOutput>> Handle(ListUsersInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
