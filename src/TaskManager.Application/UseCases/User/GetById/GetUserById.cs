using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.User.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.User.GetById;

public class GetUserById : IRequestHandler<GetUserByIdInput, UserModelOutput>
{
    private readonly IUserRepository _repository;

    public GetUserById(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserModelOutput> Handle(GetUserByIdInput request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id);
        if (user == null) throw new NotFoundException("User not found");
        return UserModelOutput.FromUser(user);

    }
}
