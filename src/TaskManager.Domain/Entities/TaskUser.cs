using TaskManager.Domain.Enums;
using TaskManager.Domain.SeedWork;
using TaskManager.Domain.Validations;

namespace TaskManager.Domain.Entities;

public class TaskUser : Entity
{
    public TaskUser(string title, string description, CategoryEnuns category, Guid userId)
    {
        Title = title;
        Description = description;
        IsCompleted = false;
        Category = category;
        UserId = userId;
        Validate();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public CategoryEnuns Category { get; private set; }
    public Guid UserId { get; private set; }


    public void UpdateTask(string? title = null
        , string? description = null,
        CategoryEnuns? category = null)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Category = category ?? Category;
        Validate();
        UpdatedAt = DateTime.UtcNow;
    }
    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsUncompleted()
    {
        IsCompleted = false;
        CompletedAt = null;
    }

    public void Validate()
    {
        DomainValidation.MinLength(Title, 3, nameof(Title));
        DomainValidation.MaxLength(Title, 50, nameof(Title));
        DomainValidation.NotNullOrEmpty(Title, nameof(Title));
        DomainValidation.MinLength(Description, 3, nameof(Description));
        DomainValidation.MaxLength(Description, 500, nameof(Description));
        DomainValidation.NotNullOrEmpty(Description, nameof(Description));
        DomainValidation.ValidateGuid(UserId, nameof(UserId));


    }



}
