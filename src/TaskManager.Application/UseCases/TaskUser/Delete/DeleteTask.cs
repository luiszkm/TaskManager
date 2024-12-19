

using TaskManager.Application.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.Delete;

public class DeleteTask : IRequestHandler<DeleteTaskInput>
{
    private readonly ITasksRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public DeleteTask(ITasksRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteTaskInput request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetById(request.Id);
        if (task == null)
        {
            throw new NotFoundException("Task not found");
        }
        var user = await _userRepository.GetById(task.UserId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        user.RemoveTask(task);
        await _taskRepository.Delete(task);
        await _userRepository.Update(user);
    }
}
