using TaskManager.Domain.SeedWork;
using TaskManager.Domain.Validations;

namespace TaskManager.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public DateTime LastUpdatePassword { get; private set; }
    public List<TaskUser> Tasks { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
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
    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
        LastUpdatePassword = DateTime.UtcNow;
        ValidateUSer();
    }
    private void ValidateUSer()
    {
        DomainValidation.MinLength(UserName, 3, nameof(UserName));
        DomainValidation.MaxLength(UserName, 50, nameof(UserName));
        DomainValidation.NotNullOrEmpty(UserName, nameof(UserName));
        DomainValidation.MinLength(Password, 3, nameof(Password));
        DomainValidation.MaxLength(Password, 50, nameof(Password));
        DomainValidation.NotNullOrEmpty(Password, nameof(Password));
    }
}

