using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Enums;
using TaskManager.Domain.SeedWork;

namespace TaskManager.Application.UseCases.TaskUser.ListTasks;

public class ListTasksInput : FilterInput, IRequest<List<TaskModelOutput>>
{
    public ListTasksInput() : base() { }

    public ListTasksInput(
        string? userName = null,
        CategoryEnuns? category = null,
        Guid? userId = null
    ) : base(userName, category, userId) { }
}
