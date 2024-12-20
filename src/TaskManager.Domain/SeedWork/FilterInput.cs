using TaskManager.Domain.Enums;

namespace TaskManager.Domain.SeedWork;

public class FilterInput
{
    public FilterInput() { }
    public FilterInput(
        string? userName = null,
        CategoryEnuns? category = null,
        Guid? userId = null
        )
    {
        UserName = userName;
        Category = category;
        UserId = userId;
    }

    public string? UserName { get; set; }
    public CategoryEnuns? Category { get; set; }
    public Guid? UserId { get; set; }

}
