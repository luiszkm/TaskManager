using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UseCases.TaskUser.Create;

public class CreateTaskInput : IRequest<TaskModelOutput>
{
    public CreateTaskInput(string title, string description, CategoryEnuns category, Guid userId)
    {
        Title = title;
        Description = description;
        Category = category;
        UserId = userId;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public CategoryEnuns Category { get; set; }
    public Guid UserId { get; set; }
}
