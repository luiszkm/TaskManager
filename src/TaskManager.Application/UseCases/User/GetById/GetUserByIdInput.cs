using TaskManager.Application.UseCases.User.Common;

namespace TaskManager.Application.UseCases.User.GetById;

public class GetUserByIdInput : IRequest<UserModelOutput>
{
    public GetUserByIdInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

}
