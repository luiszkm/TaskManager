using TaskManager.Domain.Enums;

namespace TaskManager.Domain.SeedWork;

public class FilterInput
{
    public FilterInput() { }
    public FilterInput(
        string? userName = null,
        CategoryEnuns? category = null,
        Guid? userId = null,
        int skip = 0,
        int take = 25
        )
    {
        UserName = userName;
        Category = category;
        UserId = userId;
        Skip = skip;
        Take = take;
    }

    public string? UserName { get; set; }
    public CategoryEnuns? Category { get; set; }
    public Guid? UserId { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }

}
