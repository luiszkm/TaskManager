using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.ListTasks;

public class ListTasks : IRequestHandler<ListTasksInput, List<TaskModelOutput>>
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IUserRepository _userRepository;

    public ListTasks(ITasksRepository tasksRepository, IUserRepository userRepository)
    {
        _tasksRepository = tasksRepository;
        _userRepository = userRepository;
    }

    public async Task<List<TaskModelOutput>> Handle(ListTasksInput? input, CancellationToken cancellationToken)
    {
        var tasks = await _tasksRepository.Filter(input);

        if (input.UserId != null)
        {
            var userTasks = tasks.Where(task => task.UserId == input.UserId).ToList();
            return userTasks.Select(task => TaskModelOutput.FromTask(task)).ToList();

        }


        if (input.Category != null && input.UserName != null)
        {
            var user = await _userRepository.GetByUserName(input.UserName);
            tasks = tasks.Where(task => task.Category == input.Category && task.UserId == user.Id).ToList();
            return tasks.Select(task => TaskModelOutput.FromTask(task)).ToList();
        }

        // filtragem regra de negocio 
        if (input.UserName != null)
        {
            var user = await _userRepository.GetByUserName(input.UserName);
            tasks = tasks.Where(task => task.UserId == user.Id).ToList();
            return tasks.Select(task => TaskModelOutput.FromTask(task)).ToList();
        }


        return tasks.Select(task => TaskModelOutput.FromTask(task)).ToList();

    }


}
