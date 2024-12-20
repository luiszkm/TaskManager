using TaskManager.Application.UseCases.User.Common;
using TaskManager.Domain.Authorization;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.User.Create;

public class CreateUser : IRequestHandler<CreateUserInput, UserModelOutput>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorization _authorization;

    public CreateUser(IUserRepository userRepository, IAuthorization authorization)
    {
        _userRepository = userRepository;
        _authorization = authorization;
    }

    public async Task<UserModelOutput> Handle(CreateUserInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(input.Password) || string.IsNullOrWhiteSpace(input.Password))
            throw new ApplicationException("User name is required");

        var passwordHash = _authorization.ComputeSha256Hash(input.Password);
        var user = new DomainEntity.User(input.UserName, passwordHash);
        await _userRepository.Create(user);

        return UserModelOutput.FromUser(user);
    }
}
