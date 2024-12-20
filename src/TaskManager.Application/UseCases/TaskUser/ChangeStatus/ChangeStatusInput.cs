using TaskManager.Application.UseCases.TaskUser.Common;

namespace TaskManager.Application.UseCases.TaskUser.ChangeStatus;

public class ChangeStatusInput : IRequest<TaskModelOutput>
{
    public ChangeStatusInput(Guid taskId, bool completed)
    {
        TaskId = taskId;
        Completed = completed;
    }

    public Guid TaskId { get; set; }
    public bool Completed { get; set; }
}
