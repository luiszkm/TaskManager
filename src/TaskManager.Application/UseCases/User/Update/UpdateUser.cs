using TaskManager.Application.UseCases.User.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.User.Update;

public class UpdateUser : IRequestHandler<UpdateUserInput, UserModelOutput>
{
    private readonly IUserRepository _userRepository;

    public UpdateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModelOutput> Handle(UpdateUserInput input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(input.Id);
        user.UpdatePassword(input.Password);

        await _userRepository.ChangePassword(user);

        return UserModelOutput.FromUser(user);
    }
}
