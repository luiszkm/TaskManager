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
        Guid? userId = null,
        int skip = 0,
        int take = 25

    ) : base(userName, category, userId, skip, take)
    {



    }




}
