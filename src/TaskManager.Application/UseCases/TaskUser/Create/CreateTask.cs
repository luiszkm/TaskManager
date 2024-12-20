using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.Create;

public class CreateTask : IRequestHandler<CreateTaskInput, TaskModelOutput>
{
    private readonly ITasksRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public CreateTask(ITasksRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<TaskModelOutput> Handle(CreateTaskInput input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(input.UserId);
        if (user == null)
            throw new NotFoundException("User not found");

        var task = new DomainEntity.TaskUser(input.Title, input.Description, input.Category, user.Id);
        user.AddTask(task);
        await _taskRepository.Create(task);
        await _userRepository.Update(user);
        return TaskModelOutput.FromTask(task);
    }
}
