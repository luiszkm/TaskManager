using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.Update;

public class UpdateTask : IRequestHandler<UpdateTaskInput, TaskModelOutput>
{
    private readonly ITasksRepository _tasksRepository;

    public UpdateTask(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<TaskModelOutput> Handle(UpdateTaskInput input, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetById(input.TaskId);
        task.UpdateTask(input.Title, input.Description, input.Category);
        await _tasksRepository.Update(task);



        return TaskModelOutput.FromTask(task);

    }
}
