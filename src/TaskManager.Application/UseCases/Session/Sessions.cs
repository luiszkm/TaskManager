
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Authorization;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.Session;

public class Sessions : IRequestHandler<SessionInput, SessionOutPut>
{
    private readonly IAuthorization _authorization;
    private readonly IUserRepository _userRepository;

    public Sessions(IAuthorization authorization, IUserRepository userRepository)
    {
        _authorization = authorization;
        _userRepository = userRepository;
    }

    public async Task<SessionOutPut> Handle(SessionInput input, CancellationToken cancellationToken)
    {
        var passwordHash = _authorization.ComputeSha256Hash(input.Password);
        var user = await _userRepository.GetByUserName(input.UserName);

        if (user == null) throw new ApplicationUnauthorizedException("credendital invalid");

        if (user.PasswordHash != passwordHash) throw new ApplicationUnauthorizedException("credendital invalid");

        var token = _authorization.GenerateToken(user.Id);

        return new SessionOutPut(user.Id, token);


    }
}
