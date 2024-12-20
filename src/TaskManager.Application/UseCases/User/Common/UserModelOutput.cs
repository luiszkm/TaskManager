

namespace TaskManager.Application.UseCases.User.Common;

public class UserModelOutput
{
    public UserModelOutput(
        Guid id,
        string userName,
        DateTime createdAt,
        DateTime updatedAt,
        ICollection<DomainEntity.TaskUser> tasks)
    {
        Id = id;
        UserName = userName;
        CreatedAt = createdAt;
        Tasks = tasks;
    }


    public Guid Id { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<DomainEntity.TaskUser> Tasks { get; set; }

    public static UserModelOutput FromUser(DomainEntity.User user)
    {
        return new UserModelOutput(
            user.Id,
            user.UserName,
            user.CreatedAt,
            user.UpdatedAt,
            user.Tasks.Select(x => x).ToList());

    }
}
