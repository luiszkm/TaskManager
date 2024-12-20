using TaskManager.Domain.SeedWork;
using TaskManager.Domain.Validations;

namespace TaskManager.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public string PasswordHash { get; set; }
    public DateTime LastUpdatePassword { get; private set; }
    public ICollection<TaskUser> Tasks { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public User(string userName, string passwordHash)
    {
        UserName = userName;
        PasswordHash = passwordHash;
        Tasks = new List<TaskUser>();
        ValidateUSer();
    }
    public void AddTask(TaskUser taskId)
    {
        Tasks.Add(taskId);
        ValidateUSer();
        UpdatedAt = DateTime.UtcNow;
    }
    public void RemoveTask(TaskUser taskId)
    {
        Tasks.Remove(taskId);
        ValidateUSer();
        UpdatedAt = DateTime.UtcNow;
    }

    private void ValidateUSer()
    {
        DomainValidation.MinLength(UserName, 3, nameof(UserName));
        DomainValidation.MaxLength(UserName, 50, nameof(UserName));
        DomainValidation.NotNullOrEmpty(UserName, nameof(UserName));
        DomainValidation.MinLength(PasswordHash, 3, nameof(PasswordHash));
        DomainValidation.MaxLength(PasswordHash, 500, nameof(PasswordHash));
        DomainValidation.NotNullOrEmpty(PasswordHash, nameof(PasswordHash));
    }
}

