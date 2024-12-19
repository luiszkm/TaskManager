using TaskManager.Domain.Enums;

namespace TaskManager.Domain.SeedWork;

public class FilterInput
{
    public FilterInput(
        string? userName = null,
        Guid? userId = null,
        CategoryEnuns? category = null,
        bool? completed = false)
    {
        UserName = userName;
        UserId = userId;
        Category = category;
        Completed = completed;
    }

    public string? UserName { get; set; }
    public Guid? UserId { get; set; }
    public CategoryEnuns? Category { get; set; }
    public bool? Completed { get; set; }

}
