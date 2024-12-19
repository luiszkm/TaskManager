using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UseCases.TaskUser.Update;

public class UpdateTaskInput : IRequest<TaskModelOutput>
{
    public UpdateTaskInput(Guid taskId,
        string? title = null,
        string? description = null,
        CategoryEnuns? category = null
        )
    {
        TaskId = taskId;
        Title = title;
        Description = description;
        Category = category;
    }

    public Guid TaskId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public CategoryEnuns? Category { get; set; }

}
