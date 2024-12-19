using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.Get;

public class GetTask : IRequestHandler<GetTaskInput, TaskModelOutput>
{
    private readonly ITasksRepository tasksRepository;

    public GetTask(ITasksRepository tasksRepository)
    {
        this.tasksRepository = tasksRepository;
    }
    public async Task<TaskModelOutput> Handle(GetTaskInput input, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetById(input.Id);
        if (task == null) throw new NotFoundException("Task not found");
        return TaskModelOutput.FromTask(task);
    }

}
