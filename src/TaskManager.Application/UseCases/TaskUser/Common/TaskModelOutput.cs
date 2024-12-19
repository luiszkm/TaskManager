using TaskManager.Domain.Enums;

namespace TaskManager.Application.UseCases.TaskUser.Common;

public class TaskModelOutput
{
    public TaskModelOutput(Guid id,
        string title,
        string description,
        Guid userId,
        CategoryEnuns category,
        bool isCompleted,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        Category = category;
        IsCompleted = isCompleted;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public CategoryEnuns Category { get; private set; }

    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }



    public static TaskModelOutput FromTask(DomainEntity.TaskUser task)
    {
        return new TaskModelOutput(
            task.Id,
            task.Title,
            task.Description,
            task.UserId,
            task.Category,
            task.IsCompleted,
            task.CreatedAt,
            task.UpdatedAt
            );
    }
}
