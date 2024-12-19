using TaskManager.Application.UseCases.TaskUser.Common;

namespace TaskManager.Application.UseCases.TaskUser.Get;

public class GetTaskInput : IRequest<TaskModelOutput>
{
    public GetTaskInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

}
