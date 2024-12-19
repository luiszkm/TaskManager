using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.ChangeStatus;

public class ChangeStatusTask : IRequestHandler<ChangeStatusInput, TaskModelOutput>
{
    private readonly ITasksRepository _tasksRepository;

    public ChangeStatusTask(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<TaskModelOutput> Handle(ChangeStatusInput input, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetById(input.TaskId);
        if (task == null)
        {
            throw new NotFoundException("Task Not Found");
        }

        if (input.Completed == true)
        {
            task.MarkAsCompleted();
        }
        else
        {
            task.MarkAsUncompleted();
        }

        await _tasksRepository.Update(task);

        return TaskModelOutput.FromTask(task);


    }
}
