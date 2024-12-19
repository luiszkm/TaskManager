using TaskManager.Application.UseCases.User.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.User.Create;

public class CreateUser : IRequestHandler<CreateUserInput, UserModelOutput>
{
    private readonly IUserRepository _userRepository;

    public CreateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModelOutput> Handle(CreateUserInput input, CancellationToken cancellationToken)
    {
        var user = new DomainEntity.User(input.UserName, input.Password);

        await _userRepository.Create(user);

        return UserModelOutput.FromUser(user);
    }
}
