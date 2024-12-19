using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Enums;
using TaskManager.Domain.SeedWork;

namespace TaskManager.Application.UseCases.TaskUser.ListTasks;

public class ListTasksInput : FilterInput, IRequest<List<TaskModelOutput>>
{
    public ListTasksInput(
        string? userName = null,
        Guid? userId = null,
        CategoryEnuns? category = null,
        bool completed = false) :
        base(userName, userId, category, completed)
    { }

}
